using AlterunaFPS;
using UnityEngine;

public class DeathTrap : Trap, IKillsPlayer {
    public override void Effect(PlayerController player) {
        Debug.Log("DIE");
        Player = player;
        KillPlayer(Player);
    }

    public void KillPlayer(PlayerController player) {
        // TODO: Implement way to kill affected player
        Player.GetHealth().TakeDamage(0, 10000f);
    }
}