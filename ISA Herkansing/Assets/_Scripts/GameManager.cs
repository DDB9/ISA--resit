using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Level One")]
    public GameObject exitDoor;

    int currentLevel;

    public List<Enemy> enemies = new List<Enemy>();

    Enemy[] getAllEnemies = new Enemy[0];

    // Start is called before the first frame update
    void Start()
    {
        getAllEnemies = FindObjectsOfType<Enemy>();
        if (SceneManager.GetActiveScene().name == "Level1") currentLevel = 1;
        foreach (Enemy enemy in getAllEnemies) 
        {
            enemies.Add(enemy);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel == 1) LevelOne();
    }

    public void LevelOne()
    {
        if (enemies.Count <= 0) 
        {
            exitDoor.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Finish!"); // Move on to level two.
        }
    }
}
