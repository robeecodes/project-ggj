using AlterunaFPS;
using UnityEngine;
using UnityEngine.UI;

public abstract class Trap : MonoBehaviour {
    protected bool IsActive;
    protected PlayerController Player;
    protected float Cooldown;
    
    public virtual void Effect(Player player) {}
    public virtual void EndEffect() {}

    public bool GetIsActive() {
        return IsActive;
    }
}