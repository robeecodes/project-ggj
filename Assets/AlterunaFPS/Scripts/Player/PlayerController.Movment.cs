﻿using UnityEngine;

namespace AlterunaFPS {
    public partial class PlayerController {
        [Header("Player")] public Alteruna.Avatar Avatar;

        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")] [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)] [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")] public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;


        private CharacterController _controller;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;


        // Fall Damage
        private bool _wasGrounded;
        private bool _wasFalling;
        private float _startOfFall;
        private float _minimumFall = 2f;

        [SerializeField] private AudioClip fallDamage;

        private void GroundedCheck() {
            // set sphere position, with offset
            var position = transform.position;
            Vector3 spherePosition = new Vector3(position.x, position.y - GroundedOffset, position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator) {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
            
            if (!_wasFalling && IsFalling) _startOfFall = transform.position.y;
            _wasFalling = IsFalling;
            
            if (!_wasGrounded && Grounded) {
                if (_startOfFall - transform.position.y > _minimumFall) {
                    this.GetHealth().TakeDamage(0, 1f);
                    Debug.Log(this.GetHealth().HealthPoints);
                    AudioSource.PlayClipAtPoint(fallDamage, transform.TransformPoint(_controller.center));
                }
            }

            _wasGrounded = Grounded;
        }

        bool IsFalling => (!Grounded);

        private void Move() {
            float horizontal = _horizontal;
            float vertical = _vertical;

            bool input = horizontal != 0f || vertical != 0f;

            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = 0;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            float inputMagnitude = 1f;

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (input) {
                targetSpeed = _sprint ? SprintSpeed : MoveSpeed;
                //inputMagnitude = new Vector2(horizontal, vertical).magnitude;
            }

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset) {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (input) {
                // normalise input direction
                Vector2 inputDirection = new Vector2(horizontal, vertical).normalized;

                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg +
                                  _cameraTarget.eulerAngles.y;

                if (_firstPerson) {
                    _bodyRotate = Mathf.SmoothDampAngle(_bodyRotate, _cameraTarget.eulerAngles.y, ref _rotationVelocity,
                        RotationSmoothTime);

                    transform.rotation = Quaternion.Euler(0.0f, _bodyRotate, 0.0f);

                    // play animation based on which way the character is moving relative to the characters forward
                    inputMagnitude *= vertical;
                    inputMagnitude += horizontal * (1 - Mathf.Abs(inputMagnitude));
                }
                else {
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation,
                        ref _rotationVelocity,
                        RotationSmoothTime);

                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if (_hasAnimator) {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity() {
            if (Grounded) {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator) {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f) {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_jump && _jumpTimeoutDelta <= 0.0f) {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator) {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f) {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f) {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else {
                    // update animator if using character
                    if (_hasAnimator) {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                //_input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity) {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }
    }
}