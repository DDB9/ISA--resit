using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    // Spawnpoints
    public int health = 10;

    // Use this for initialization
    void Start ()
    {
		
	}

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
