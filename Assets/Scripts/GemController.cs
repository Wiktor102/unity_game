using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour {
    void OnTriggerEnter2D (Collider2D col) {
        if (col.name == "Player") {
           var gemsController = GetComponentInParent<GemSpawnerController>();
           gemsController.DeleteGem(gameObject);
        }
    }
}
