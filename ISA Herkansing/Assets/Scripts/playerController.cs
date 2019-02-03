using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public static playerController instance = null;

    public static int playerLives = 5;
    public float walkSpeed = 20f;
    public float sprintSpeed;
    public GameObject gun;

    [SerializeField]
    private float speed = 20;

    Animator gunAnimator;

    // Use this for initialization
    void Start()
    {
        sprintSpeed = walkSpeed * 1.50f;
        Cursor.lockState = CursorLockMode.Locked;
        gunAnimator = gun.GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update ()
    {          
        // PLAYER MOVEMENT
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        // Makes the cursor re-appear for menu purposes.
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        // If left-shift is pressed, player runs. Else, it walks.
        if (Input.GetKey(KeyCode.LeftShift)) speed = sprintSpeed;
        else speed = walkSpeed;

        // Plays the shooting animation for the gun if the player clicks the left mouse button.
        if (Input.GetMouseButtonDown(0)) gunAnimator.SetInteger("gun_state", 1);
	}

    public void resetIdle()
	{
        // Resets the animation state to the idle animation.
		gunAnimator.SetInteger("gun_state", 0);
	}

}
