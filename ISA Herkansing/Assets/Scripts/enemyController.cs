using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

	public static enemyController instance = null;

	public GameObject player;
	public RigidBody enemyRBody;
	public float enemySpeed;
	public float distanceFromPlayer;
	public float meleeRange;
	public float attackRate;
	public float meleeTimer;
	public bool playerInSight;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		enemyRBody = GetComponent<RigidBody>();
		meleeTimer = Time.time += attackRate;
		playerInSight = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerInRange(){
		Vector3 playerPos = player.transform.position;
		playerPos.y = transform.position.y;
		transform.LookAt(playerPos);

		if (distanceFromPlayer >= meleeRange){
			transform.position += transform.forward * enemySpeed * Time.deltaTime;
		}
		if (distanceFromPlayer <= meleeRange){
			// do damage.
		}
	}
}
