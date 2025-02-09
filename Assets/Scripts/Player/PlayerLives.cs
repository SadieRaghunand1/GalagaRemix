using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private GameManager gameManager;
    int lives = 3;
    [SerializeField] private Animator animator;
    [SerializeField] private Image[] healthUI;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }


    public void GetHit()
    {
        Debug.Log("Player Hit");

        --lives;
        healthUI[lives].enabled = false;
        playerMovement.BecomeSingle();
        //Play explosion animation
        //animator.SetTrigger()

        if (lives <= 0)
        {
            //Game over
            Debug.Log("Game over :(");
            gameManager.InitializeHighScore();
            LoadLoseScene();
        }

    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene(5);
    }


}
