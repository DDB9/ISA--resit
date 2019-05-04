using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public PlayerController player;
    public Enemy target;
    public GameManager manager;

    public enum State { Idle, Attack, Move }
    public State currentState;

    public float enemySpeed = 10f;
    public float meleeRange;
    public float senseRange = 5f; // Adjust this if the ghosts start to follow the player right away.

    // Declaring attack types.
    public bool meleeAttack = false;
    public bool rangedAttack = false;

    public bool attacked = false;

    [SerializeField]
    public float distanceFromPlayer;

    private NavMeshAgent agent;

    private void Start() 
    {
        player = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    } 

    public virtual void CheckState()
    {
        switch (currentState)
        { // DECLARE STATE SWITCH CASES.
            case State.Idle:    // RETURN ALL ENEMIES BACK TO THEIR ORIGINAL POSITION.
                if (transform.name == "Enemy_Ghost1") 
                {
                    agent.SetDestination(transform.Find("GSP1").position);
                    break;
                }
                if (transform.name == "Enemy_Ghost2")
                {
                    agent.SetDestination(transform.Find("GSP2").position);
                    break;
                }
                if (transform.name == "Enemy_Ghost3")
                {
                    agent.SetDestination(transform.Find("GSP3").position);
                    break;
                }

            break;
            
            case State.Move:
                if (GetComponent<Ghost>().playerInSight == true){
                    GetComponent<Ghost>().Movement();
                    break;
                }

            break;
        }
    }

    IEnumerator DealDamageOverTime()
    {
        if (meleeAttack)
        {
            Debug.Log("Attack!");
            player.playerLives -= 1;
            player.imagePlayerLives[player.imagePlayerLives.Count - 1].enabled = false;
            player.imagePlayerLives.RemoveAt(player.imagePlayerLives.Count - 1);
            Debug.Log(player.playerLives.ToString());
            
            yield return new WaitForSeconds(1.2f);

            meleeAttack = false;
            attacked = false;
        }

    }
}
                                        