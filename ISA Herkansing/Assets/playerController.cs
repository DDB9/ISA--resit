﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController: MonoBehaviour {
	
	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	GameObject player;
	Animator gunAnimator;

	// Use this for initialization
	void Start (){
		Cursor.lockState = CursorLockMode.Locked;

		player = this.transform.parent.gameObject;
		gunAnimator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update (){
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		mouseLook += smoothV;

		mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

		transform.localRotation = Quaternion.AngleAxis(angle: -mouseLook.y, axis: Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);

		if (Input.GetMouseButton(0)){ 
			gunAnimator.SetInteger("gun_state", 1);
			if (this.gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("gun_shoot")) gunAnimator.SetInteger("gun_state", 2); // This prevents anything from happening while the animation is playing,
																															// Which is not exactly what we want. We want the animation to play, then switch back.
		}
	}
}