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

    [Header("FadeIn/Out")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float alphaChange = 0.1f;
     

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
        StartCoroutine(FadeOut());

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


    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.1f);
        Color _fade = spriteRenderer.color;
        _fade.a -= alphaChange;
        spriteRenderer.color = _fade;

        if(spriteRenderer.color.a > 0)
            StartCoroutine(FadeOut());

        else if(spriteRenderer.color.a <= 0)
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.1f);
        Color _fade = spriteRenderer.color;
        _fade.a += alphaChange;
        spriteRenderer.color = _fade;

        if( spriteRenderer.color.a < 1)
            StartCoroutine(FadeIn());
    }

}
