using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void mainMenu()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);

    }

    public void quit()
    {
        audioSource.Play();
        Application.Quit();
    }
}
