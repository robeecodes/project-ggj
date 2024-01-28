using AlterunaFPS;
using UnityEngine;


public class DeathTrap : Trap, IKillsPlayer {

    public AudioSource explodSound;

    public override void Effect(PlayerController player) {
        Player = player;
        explodSound.Play();
        KillPlayer(Player);
    }

    public void KillPlayer(PlayerController player) {
        Player.GetHealth().TakeDamage(0, 20f);
    }
}