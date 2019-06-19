using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    private void Start() {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update() {
        CheckState();
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (distanceFromPlayer <= engageDistance && distanceFromPlayer > meleeRange) {
            currentState = EnemyState.ENGAGE;
        }
        else if (distanceFromPlayer > engageDistance) {
            currentState = EnemyState.IDLE;
        }
        
    }
}
