using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy 
{
    public bool playerInSight;

    public int health = 10;

    private void Start() 
    {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update() 
    {    // Update is called once per frame
        if (playerInSight == true) currentState = State.Move;
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        CheckState();
    }

    public virtual void Movement()  // Movement, hello?
    {
        Debug.Log("In sight!");
        Vector3 playerPos = player.transform.position; 
        playerPos.y = transform.position.y; // Make sure the object doesn't stat flying.
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
