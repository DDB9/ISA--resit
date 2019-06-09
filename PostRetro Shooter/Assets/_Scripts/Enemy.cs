using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState {IDLE, ENGAGE, ATTACK}

public class Enemy : MonoBehaviour {

    public PlayerController player;
    public GameManager manager;

    public EnemyState currentState;
    public float enemySpeed = 10f;
    public float meleeRange;

    public bool meleeAttack = false;
    // private bool rangedAttack = false;

    public bool attacked = false;

    public float distanceFromPlayer;
    public float engageDistance;

    public static bool playerInSight;

    private void Start() {
        player = FindObjectOfType<PlayerController>();
        currentState = EnemyState.IDLE;
    }   

    public virtual void Movement() {

        currentState = EnemyState.ENGAGE;
        Vector3 playerPos = player.transform.position;
        playerPos.y = transform.position.y;
        transform.LookAt(playerPos);

        if (distanceFromPlayer > meleeRange) 
        {
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }

        if (distanceFromPlayer <= meleeRange) {
            if (player.playerLives > 0) {
                meleeAttack = true;
                currentState = EnemyState.ATTACK;
            }
        }
    }

    public virtual IEnumerator DealDamageOverTime() {
        if (meleeAttack) {
            Debug.Log("Attack!");
            player.playerLives -= 1;
            player.imagePlayerLives[player.imagePlayerLives.Count - 1].enabled = false;
            player.imagePlayerLives.RemoveAt(player.imagePlayerLives.Count - 1);

            yield return new WaitForSeconds(1.2f);

            meleeAttack = false;
            attacked = false;
        }
    }
}
                                        