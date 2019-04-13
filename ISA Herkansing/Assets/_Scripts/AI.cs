using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum State { Idle, Move, Attack }

public class AI : MonoBehaviour {

    public State currentState;
    public float attackRange;
    public float maxCooldown = 3;
    public float senseRange = 10;

    private NavMeshAgent agent;
    private Enemy target;
    private float coolDown;
    private float distanceToTarget;

    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {

        CheckState();
    }

    void CheckState() {

        // Sensing the target.
        if (target == null) {
            distanceToTarget = float.MaxValue;

            Collider[] cols = Physics.OverlapSphere(transform.position, senseRange);
            foreach (Collider c in cols) {
                if (c.gameObject == gameObject) {
                    continue;
                }

                Enemy hp = c.gameObject.GetComponent<Ghost>();
                if (hp != null) {
                    Debug.Log("Health found!");
                    float distanceToEnemy = Vector3.Distance(transform.position, hp.transform.position);
                    if (distanceToEnemy < distanceToTarget) {
                        target = hp;
                        distanceToTarget = distanceToEnemy;
                    }
                }
            }

            if (target == null) {
                currentState = State.Idle;
            }
        }

        else {
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distanceToTarget > senseRange) {
                target = null;
            }
        }

        //AI States
        switch (currentState) {
            case State.Attack:

                // Waiting for the cooldown
                if (coolDown > 0) {
                    coolDown -= Time.deltaTime;
                }

                //Do Damage
                if (distanceToTarget < attackRange && coolDown <= 0) {
                    target.StartCoroutine("DealDamageOverTime");
                    coolDown = maxCooldown;
                }

                //Transition
                if (distanceToTarget > 2 * attackRange) {
                    currentState = State.Move;
                }

                break;

            case State.Idle:

                //If the agent is to close to something that is no the player, choose a new destination
                if (agent.remainingDistance > agent.stoppingDistance) {
                    break;
                }
                else {
                    agent.SetDestination(transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
                }

                // If the target is within the sensing range...
                if (distanceToTarget < senseRange) {
                    currentState = State.Move;
                }


                break;

            case State.Move:
                //Move to the target
                if (target != null) {
                    agent.SetDestination(target.transform.position);
                }


                if (distanceToTarget < attackRange) {
                    currentState = State.Attack;
                }

                break;
        }
    }
}
