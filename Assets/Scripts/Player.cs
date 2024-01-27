using System;
using System.Collections;
using System.Collections.Generic;
using AlterunaFPS;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private LayerMask defaultLayerMask;

    private void Update() {
        HandleInteractions();
    }

    private void HandleInteractions() {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, .2f, defaultLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out Trap trap)) {
                if (!trap.GetIsActive()) {
                    trap.Effect(this.GetComponent<PlayerController>());
                }
            }
        }
    }
}