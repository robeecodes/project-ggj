using AlterunaFPS;
using UnityEngine;

// This does not work
public class RotateTrap : Trap {
    private void Start() {
        Cooldown = 1f;
    }
    
    private void Update() {
        if (IsActive && Cooldown > 0) {
            Cooldown -= Time.deltaTime;
        }
    
        if (Cooldown < 0f) {
            Cooldown = 1f;
            IsActive = false;
        }
    }
    
    public override void Effect(PlayerController player) {
        Debug.Log("Rotate");
        Player = player;
        Player.transform.RotateAround(Player.transform.position, Vector3.up, 180);
        IsActive = true;
    }
}