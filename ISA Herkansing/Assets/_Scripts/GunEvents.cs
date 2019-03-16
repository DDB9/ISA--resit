using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEvents : MonoBehaviour {

	Animator parentAnim;

	void Start() {
		parentAnim = GetComponentInParent<Animator>();
	}

	public void ResetToZero() {
		parentAnim.SetInteger("gun_state", 0);
	}
}
