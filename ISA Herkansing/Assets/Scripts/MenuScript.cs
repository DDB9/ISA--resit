using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
