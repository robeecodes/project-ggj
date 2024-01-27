using System;
using System.Collections;
using System.Collections.Generic;
using AlterunaFPS;
using UnityEngine;

public class RotateTrap : Trap {
    private bool _hasSpun;

    private void Start() {
        Cooldown = 1f;
    }

    private void Update() {
        if (_hasSpun && Cooldown > 0) {
            Cooldown -= Time.deltaTime;
        }

        if (Cooldown < 0f) {
            Cooldown = 1f;
            _hasSpun = false;
        }
    }

    public override void Effect(PlayerController player) {
        if (!_hasSpun) {
            Player = player;
            // Player.transform.RotateAround(Player.transform.position, Vector3.up, 180);
            // Player.RotateCamera180Degrees();
            _hasSpun = true;   
            
        }
    }
}
