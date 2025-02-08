using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    int lives = 3;
    [SerializeField] private Animator animator;
    [SerializeField] private Image[] healthUI;
    

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
        }

    }
}
