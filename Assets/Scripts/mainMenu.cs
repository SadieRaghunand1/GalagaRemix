using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        if(FindAnyObjectByType<GameManager>() == null)
        {
            Instantiate(gameManagerPrefab);
        }
    }

    public void startGame()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        audioSource.Play();
        Application.Quit();
    }

    public void settings()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void howToPlay()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    
}
