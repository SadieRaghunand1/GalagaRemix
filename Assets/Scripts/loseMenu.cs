using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loseMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void mainMenu()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);

    }

    public void quit()
    {
        audioSource.Play();
        Application.Quit();
    }
}
