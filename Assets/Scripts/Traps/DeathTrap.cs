using AlterunaFPS;
using UnityEngine;


public class DeathTrap : Trap, IKillsPlayer {
    public AudioSource explosionSound;
    public override void Effect(PlayerController player) {
        Player = player;
        KillPlayer(Player);
        explosionSound.Play();
    }

    public void KillPlayer(PlayerController player) {
        Player.GetHealth().TakeDamage(0, 20f);
    }
}