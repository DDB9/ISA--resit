using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject exitDoor;
    public List<Enemy> enemies = new List<Enemy>();

    int currentLevel;
    Enemy[] getAllEnemies = new Enemy[0];

    // Start is called before the first frame update
    void Start() {
        getAllEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in getAllEnemies) {
            enemies.Add(enemy);
        }

        if (SceneManager.GetActiveScene().name == "Level1") currentLevel = 1;
        if (SceneManager.GetActiveScene().name == "Level2") currentLevel = 2;
    }

    // Update is called once per frame
    void Update() {
        if (enemies.Count <= 0) {
            exitDoor.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && currentLevel == 1) {
            SceneManager.LoadScene("Level2");
        }
        if (other.CompareTag("Player") && currentLevel == 2){
            Debug.Log("Finish!");
        }
    }
}
