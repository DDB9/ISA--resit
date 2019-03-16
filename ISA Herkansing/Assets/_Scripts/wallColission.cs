using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallColission : MonoBehaviour {

    public GameObject player;

    private void OnTriggerStay(Collider other)
    {
        player.transform.position = player.transform.position;
    }
}
