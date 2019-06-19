using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    private void Start() {
        PlayerController.OnEnemyHit += ChangeColorOnHit;    // subscribes the damage method to the onEnemyHit event.

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

    public void ChangeColorOnHit(Enemy enemy) {    // Why isn't IEnumerator possible? Invoke instead of simply calling?
        StartCoroutine("ChangeColor", enemy);
    }

    public IEnumerator ChangeColor(Enemy enemy) {
        if (this != null) {
            enemy.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            enemy.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnDisable() {
        PlayerController.OnEnemyHit -= ChangeColorOnHit;
    }
}