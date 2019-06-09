using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    public int health = 10;

    private void Start() {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update() {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (distanceFromPlayer <= engageDistance && distanceFromPlayer > meleeRange) {
            currentState = EnemyState.ENGAGE;
        }
        else if (distanceFromPlayer > engageDistance) {
            currentState = EnemyState.IDLE;
        }

        switch (currentState) {
            case EnemyState.IDLE:
                // do nothing, for now.
                // might send them back to spawnpoint?
                break;

            case EnemyState.ENGAGE:
                Movement();
                break;

            case EnemyState.ATTACK:
                if (!attacked) {
                    StartCoroutine("DealDamageOverTime");
                    attacked = true;
                }
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0) 
        {
            Destroy(this.gameObject);
            manager.enemies.RemoveAt(0);
        }
    }
}
