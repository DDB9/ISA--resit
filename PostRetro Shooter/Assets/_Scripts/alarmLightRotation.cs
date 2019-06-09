using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alarmLightRotation : MonoBehaviour {

	public float rotSpeed = 10;

    // Once per frame, the light rotates a degree to the right, varying through rotSpeed;
    void Update() { transform.Rotate(Vector3.right * Time.deltaTime * rotSpeed); }
}
