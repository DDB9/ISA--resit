using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public static Enemy instance = null;

	public GameObject player;
    public float enemySpeed;
	public float meleeRange;
	public float attackRate;
	public float meleeTimer;
	
    [SerializeField]
	private float distanceFromPlayer;
	private bool playerInSight;

	// Use this for initialization
	void Start ()
    {
		player = GameObject.FindWithTag("Player");
		//meleeTimer = Time.time + attackRate;
		playerInSight = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (playerInSight == true) PlayerInRange();
        else if (playerInSight == false) enemySpeed = 0;
	}

    void OnTriggerStay(Collider other) { if (other.tag == "Player") playerInSight = true; } 

	void OnTriggerExit(Collider other) { if (other.tag == "Player") playerInSight = false; }

	public void PlayerInRange()
    {
		Vector3 playerPos = player.transform.position;
		playerPos.y = transform.position.y;
		transform.LookAt(playerPos);

		if (distanceFromPlayer >= meleeRange) transform.position += transform.forward * enemySpeed * Time.deltaTime;
        if (distanceFromPlayer <= meleeRange) dealDamage();

    public virtual void dealDamage()
    {
        if (Time.time > attackRate)
        {
            Debug.Log("Attack!");
            playerController.playerLives -= 1;
            Debug.Log(playerController.playerLives.ToString());
            if (playerController.playerLives <= 0) Debug.Log("YOU DIED");
        }
    }
}
                                        