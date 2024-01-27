using AlterunaFPS;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private LayerMask defaultLayerMask;

    private void Update() {
        HandleInteractions();
    }

    private void HandleInteractions() {
        // Player will search for collisions underfoot
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, .2f, defaultLayerMask)) {
            // Enable trap effect if Player steps on a trap
            if (raycastHit.transform.TryGetComponent(out Trap trap)) {
                if (!trap.GetIsActive()) {
                    trap.Effect(this.GetComponent<PlayerController>());
                }
            }
        }
    }
}