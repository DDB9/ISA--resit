using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance = null;
    public GameManager manager;
    public int playerLives = 5;
    public int ammo = 10;
    public List<Image> imagePlayerLives = new List<Image>();
    [Space]
    public Image bloodstains;
    public Text ammoDisplay;
    public float walkSpeed = 20f;
    public float sprintSpeed;
    [SerializeField]
    private float speed = 20;

    public GameObject gun;
    private Animator gunAnimator;

    public delegate void ChangeEnemyColor(Enemy enemy); // declaring the damage delegate...
    public static event ChangeEnemyColor OnEnemyHit;       // ... and event.

    private bool hasHit;
    private bool hasAmmo = true;

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

        // makes sure you cant fire the gun when you are out of ammo. press r to reload.
        ammoDisplay.text = ammo.ToString();
        if (ammo <= 0) hasAmmo = false;
        if (Input.GetKeyDown("r")) StartCoroutine("ReloadDelay");

        // Makes the cursor re-appear for menu purposes. REMOVE WHEN BUILDING.
        if (Input.GetKeyDown("escape")) Cursor.lockState = CursorLockMode.None;

        // If left-shift is pressed, player runs. Else, it walks.
        if (Input.GetKey(KeyCode.LeftShift)) speed = sprintSpeed;
        else speed = walkSpeed;

        // Plays the shooting animation for the gun if the player clicks the left mouse button.
        if (Input.GetMouseButtonDown(0) && hasAmmo) {
            if (gunAnimator.GetInteger("gun_state") == 0) gunAnimator.SetInteger("gun_state", 1);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.CompareTag("Enemy") && !hasHit) {
                    hasHit = true;

                    if (OnEnemyHit != null) {               // If event has listeners, then execute said event.
                        OnEnemyHit(hit.transform.GetComponent<Enemy>());
                    }

                    Enemy ghost = hit.transform.GetComponent<Enemy>();
                    ghost.TakeDamage(3);                                // deals this much damage to the enemy if the player shot them.
                    StartCoroutine("ShootDelay");                       // inflicts a delay so that the player does not actually shoot when the animation is still playing.
                }
                if (!hit.transform.CompareTag("Enemy") && !hasHit) {
                    hasHit = true;
                    StartCoroutine("ShootDelay");
                }
            }

            // resets the shooting animation after shooting.
            if (gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("gun_shoot")) gunAnimator.SetInteger("gun_state", 0);
        }

        // visualizing playerlives. Will try to make this more efficient.
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
        // load game over screen when the player lost all lives.
        if (playerLives <= 0) SceneManager.LoadScene("Game Over");
    }

    // a delay for when the gun has been shot.
    IEnumerator ShootDelay() {
        ammo -= 1;
        yield return new WaitForSeconds(0.5f);
        hasHit = false;
    }

    // a delay for when reloading the gun.
    IEnumerator ReloadDelay() {
        yield return new WaitForSeconds(1);
        ammo = 10;
        hasAmmo = true;
    }
}
