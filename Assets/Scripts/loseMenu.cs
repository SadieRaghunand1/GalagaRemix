using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loseMenu : MonoBehaviour
{
    public void mainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);

    }

    public void quit()
    {
        Application.Quit();
    }
}
