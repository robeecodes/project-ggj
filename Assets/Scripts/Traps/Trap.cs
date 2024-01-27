using AlterunaFPS;
using UnityEngine;

public abstract class Trap : MonoBehaviour {
    protected bool IsActive = false;
    protected PlayerController Player;
    protected float Cooldown;
    
    public virtual void Effect(PlayerController player) {}
    
    public virtual void EndEffect() {}

    public bool GetIsActive() {
        return IsActive;
    }
}