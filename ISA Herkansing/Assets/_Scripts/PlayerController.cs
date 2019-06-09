using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance = null;

    public int playerLives = 5;
    public List<Image> imagePlayerLives = new List<Image>();
    public Image bloodstains;
    public float walkSpeed = 20f;
    public float sprintSpeed;
    [SerializeField]
    private float speed = 20;

    public GameObject gun;
    private Animator gunAnimator;

    // Use this for initialization
    void Start() {
        sprintSpeed = walkSpeed * 1.50f;
        Cursor.lockState = CursorLockMode.Locked;
        gunAnimator = gun.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
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
        if (Input.GetKey(KeyCode.LeftShift))
            speed = sprintSpeed;
        else
            speed = walkSpeed;

        // Plays the shooting animation for the gun if the player clicks the left mouse button.
        if (Input.GetMouseButtonDown(0)) {
            gunAnimator.SetInteger("gun_state", 1);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9)) {
                if (hit.transform.CompareTag("Enemy")) {
                    Ghost ghost = hit.transform.GetComponent<Ghost>();
                    ghost.TakeDamage(3);    // deals this much damage to the enemy if the player shot them.
                }
            }
        }
        //Visualizing playerlives. Will try to make this more efficient.
        if (playerLives >= 5) {
            var tempColor = bloodstains.color;
            tempColor.a = 0f;
            bloodstains.color = tempColor;
        }
        if (playerLives == 4) {
            var tempColor = bloodstains.color;
            tempColor.a = 0.1f;
            bloodstains.color = tempColor;
        }
        if (playerLives == 3) {
            var tempColor = bloodstains.color;
            tempColor.a = 0.25f;
            bloodstains.color = tempColor;
        }
        if (playerLives == 2) {
            var tempColor = bloodstains.color;
            tempColor.a = 0.45f;
            bloodstains.color = tempColor;
        }
        if (playerLives == 1) {
            var tempColor = bloodstains.color;
            tempColor.a = 0.65f;
            bloodstains.color = tempColor;
        }
        if (playerLives <= 0) {
            SceneManager.LoadScene("Game Over");
        }
    }

    public void resetIdle() {
        // Resets the animation state to the idle animation.
        gunAnimator.SetInteger("gun_state", 0);
    }

}
