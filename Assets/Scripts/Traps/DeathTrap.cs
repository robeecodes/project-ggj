using AlterunaFPS;

public class DeathTrap : Trap, IKillsPlayer {
    public override void Effect(PlayerController player) {
        Player = player;
        KillPlayer(Player);
    }

    public void KillPlayer(PlayerController player) {
        Player.GetHealth().TakeDamage(0, 20f);
    }
}