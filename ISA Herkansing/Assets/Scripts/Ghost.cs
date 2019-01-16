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
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerOORange();
            playerInSight = false;
        }
    }

    private void PlayerOORange()
    {
        if (GameObject.Find("Enemy_Ghost1")) transform.LookAt(ghostSP1.transform);
        else if (GameObject.Find("Enemy_Ghost2")) transform.LookAt(ghostSP2.transform);
        else if (GameObject.Find("Enemy_Ghost3")) transform.LookAt(ghostSP3.transform);
        Vector3.MoveTowards(this.transform.position, ghostSP1.transform.position, enemySpeed * Time.deltaTime);
    }
}
