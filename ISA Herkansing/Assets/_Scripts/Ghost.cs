using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy 
{

    public int health = 10;

    private void Start() 
    {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update() 
    {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
    }

    public override void Movement()
    {
        base.Movement();
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
