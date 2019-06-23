using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState {IDLE, ENGAGE, ATTACK, HATCH}

public class Enemy : MonoBehaviour {

    public PlayerController player;
    public GameManager manager;
    
    public float health = 10f;

    public EnemyState currentState;
    public float enemySpeed = 10f;
    public float meleeRange;

    public bool attacked = false;

    public float distanceFromPlayer;
    public float engageDistance;

    public static bool playerInSight;

    private void Start() {
        player = FindObjectOfType<PlayerController>();
        manager = FindObjectOfType<GameManager>();

        currentState = EnemyState.IDLE;
    }   

    public virtual void Movement() {
        Vector3 playerPos = player.transform.position;
        playerPos.y = transform.position.y;
        transform.LookAt(playerPos);

        if (distanceFromPlayer > meleeRange) {
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }

        if (distanceFromPlayer <= meleeRange) {
            if (player.playerLives > 0) {
                currentState = EnemyState.ATTACK;
            }
        }
    }

    public virtual IEnumerator DealDamageOverTime() {
        player.playerLives -= 1;
        player.imagePlayerLives[player.imagePlayerLives.Count - 1].enabled = false;
        player.imagePlayerLives.RemoveAt(player.imagePlayerLives.Count - 1);

        yield return new WaitForSeconds(1.2f);

        attacked = false;
        currentState = EnemyState.ENGAGE;
    }

    public virtual void TakeDamage(float damage) {
        health = health - damage;
        if (health <= 0) {
            manager.enemies.RemoveAt(0);
            Destroy(this.gameObject);
        }
    }

    public virtual void CheckState() {
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
}
                                        