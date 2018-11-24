using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {

    // Spawnpoints
    public GameObject ghostSP1;
    public GameObject ghostSP2;
    public GameObject ghostSP3;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    private void OnTriggerExit(Collider other) { if (other.tag == "Player") PlayerOORange(); }

    private void PlayerOORange()
    {
        if (GameObject.Find("Enemy1")) transform.LookAt(ghostSP1.transform);
        else if (GameObject.Find("Enemy2")) transform.LookAt(ghostSP2.transform);
        else if (GameObject.Find("Enemy3")) transform.LookAt(ghostSP3.transform);
        transform.position += transform.forward * enemySpeed * Time.deltaTime;
    }
}
