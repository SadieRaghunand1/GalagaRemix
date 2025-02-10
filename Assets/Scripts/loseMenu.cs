using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loseMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI scoretext;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.enemiesKilled = 0;
        scoretext.text = "Score: " + gameManager.score;
    }

    public void mainMenu()
    {
        audioSource.Play();
        gameManager.stage = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);

    }

    public void quit()
    {
        audioSource.Play();
        Application.Quit();
    }
}
