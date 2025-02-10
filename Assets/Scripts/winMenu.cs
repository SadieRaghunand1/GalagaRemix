using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class winMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI scoretext;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        scoretext.text = "Score: " + gameManager.score;
    }

    public void mainMenu()
    {
        audioSource.Play();
        gameManager.stage = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);

    }

    public void quit()
    {
        audioSource.Play();
        Application.Quit();
    }
}
