using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public PlayerController player;
    public GameManager manager;

    public float enemySpeed = 10f;
    public float meleeRange;

    // Declaring attack types.
    private bool meleeAttack = false;
    private bool rangedAttack = false;

    private bool attacked = false;

    [SerializeField]
    public float distanceFromPlayer;

    public static bool playerInSight;

    private void Start() 
    {
        player = FindObjectOfType<PlayerController>();
    }   
    
    // Update is called once per frame
    void Update()
    {
        if (playerInSight == true) Movement();
    }

    void OnTriggerStay(Collider other) { if (other.tag == "Player") playerInSight = true; }

    void OnTriggerExit(Collider other) { if (other.tag == "Player") playerInSight = false; }

    public virtual void Movement()  // Movement, hello?
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y = transform.position.y;
        transform.LookAt(playerPos);

        if (distanceFromPlayer > meleeRange) 
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

            meleeAttack = false;
            attacked = false;
        }

    }
}
                                        