using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDetection : MonoBehaviour
{
    void OnTriggerStay(Collider other) { if (other.CompareTag("Player")) transform.parent.GetComponent<Ghost>().playerInSight = true; }

    void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) transform.parent.GetComponent<Ghost>().playerInSight = false; }
}
