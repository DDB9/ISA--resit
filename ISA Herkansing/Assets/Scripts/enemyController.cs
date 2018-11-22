using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

	public static enemyController instance = null;

	public GameObject player;
	public Rigidbody enemyRBody;
	public float enemySpeed;
	public float meleeRange;
	public float attackRate;
	public float meleeTimer;
	
	private float distanceFromPlayer;
	private bool playerInSight;

	// Use this for initialization
	void Start () {
		distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

		player = GameObject.FindWithTag("Player");
		enemyRBody = GetComponent<Rigidbody>();
		meleeTimer = Time.time + attackRate;
		playerInSight = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInSight == true) PlayerInRange();
	}

	void OnTriggerStay(Collider other) { 
		if (other.tag == "Player"){
			playerInSight = true; 
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "player"){
			playerInSight = false; 
		}
	}

	public void PlayerInRange(){
		Vector3 playerPos = player.transform.position;
		playerPos.y = transform.position.y;
		transform.LookAt(playerPos);

		if (distanceFromPlayer >= meleeRange) transform.position += transform.forward * enemySpeed * Time.deltaTime;
		if (distanceFromPlayer <= meleeRange){
			// do damage.
		}
	}
}
