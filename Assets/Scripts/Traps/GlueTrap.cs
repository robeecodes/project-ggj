using AlterunaFPS;
using UnityEngine;

// Trap halves the speed of the player for the specified cooldown time.
public class GlueTrap : Trap
{
    public AudioSource glueSound;
    public bool alreadyPlaying = false;

    private void Start() {
        // Cooldown is 5s
        Cooldown = 5f;

    }


    private void Update() {
        // When activated, the countdown begins
        if (Cooldown > 0 && IsActive) {
            Cooldown -= Time.deltaTime;
        }
        // When the countdown has ended, reset the timer
        else if (Cooldown <= 0) {
            Cooldown = 5f;
            if (Player != null) {
                EndEffect();
            }
        }
        
    }

   

    // This effect slows the player down
    public override void Effect(PlayerController player) {
        IsActive = true;
        glueSound.Play();
        Player = player;
        Player.MoveSpeed /= 2f;
        Player.SprintSpeed /= 2f;
    }

    // Return the speeds to normal when complete
    public override void EndEffect() {
        Player.MoveSpeed *= 2f;
        Player.SprintSpeed *= 2f;
        Player = null;
        IsActive = false;
    }
}
