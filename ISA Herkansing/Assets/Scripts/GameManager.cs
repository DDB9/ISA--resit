using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int currentLevel;
    List<Enemy> enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1") currentLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel == 1) LevelOne();
    }

    public void LevelOne()
    {
        
    }
}
