using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public static Enemy instance = null;

    private playerController player;
    
    public float enemySpeed = 10f;
    public float meleeRange;
    public float attackRate;
    public float meleeTimer;

    // Declaring attack types.
    private bool meleeAttack = false;
    private bool rangedAttack = false;

    private bool attacked = false;

    [SerializeField]
    private float distanceFromPlayer;

    public static bool playerInSight;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindObjectOfType<playerController>();
        //meleeTimer = Time.time + attackRate;
        playerInSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (playerInSight == true) PlayerInRange();
    }

    void OnTriggerStay(Collider other) { if (other.tag == "Player") playerInSight = true; }

    void OnTriggerExit(Collider other) { if (other.tag == "Player") playerInSight = false; }

    public void PlayerInRange()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y = transform.position.y;
        transform.LookAt(playerPos);

        if (distanceFromPlayer >= meleeRange) 
        {
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }

        if (distanceFromPlayer <= meleeRange)
        {
            if (player.playerLives >= 0)
            {
                meleeAttack = true;
                if (!attacked)
                {
                    StartCoroutine("DealDamageOverTime");
                    attacked = true;
                }
            }
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

            if (player.playerLives == 0) Debug.Log("YOU DIED");

            meleeAttack = false;
            attacked = false;
        }

    }
}
                                        