using AlterunaFPS;
using UnityEngine;

public class FOVTrap : Trap {
    private float _random;
    
    private void Start() {
        Cooldown = 3f;
    }

    private void Update() {
        // When activated, the countdown begins
        if (Cooldown > 0 && IsActive) {
            Debug.Log(Cooldown);
            Cooldown -= Time.deltaTime;
            CinemachineVirtualCameraInstance.Instance.SetFov(_random);
        }
        // When the countdown has ended, reset the timer
        else if (Cooldown <= 0) {
            Cooldown = 3f;
            EndEffect();
        }
    }

    // Set the camera FOV to between 5 and 200
    public override void Effect(PlayerController player) {
        _random = Random.Range(5f, 200f);
        CinemachineVirtualCameraInstance.Instance.SetFov(_random);
        IsActive = true;
    }

    // Reset FOV
    public override void EndEffect() {
        CinemachineVirtualCameraInstance.Instance.ResetFov();
        IsActive = false;
    }
}