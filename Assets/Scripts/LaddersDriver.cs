﻿using UnityEngine;

public class LaddersDriver : MonoBehaviour {
    private readonly string _playerName = "Player";


    void OnTriggerStay2D (Collider2D other) {

        if (IsPlayer(other)) {
            var script = other.GetComponentInParent<PlayerController>();
            var vel = other.GetComponent<Rigidbody2D>().velocity;

            if (Input.GetKey(KeyCode.UpArrow)){
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(vel.x, script.verticalPlayerSpeed);
                script.IsClimbing(true);
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(vel.x, -script.verticalPlayerSpeed);
                script.IsClimbing(true);
            } else {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(vel.x, 0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            var script = other.GetComponentInParent<PlayerController>();
            script.IsClimbing(false);
        }
    }

    private bool IsPlayer(Collider2D other) => other.name == _playerName;
}
