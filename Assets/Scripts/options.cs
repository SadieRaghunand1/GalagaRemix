using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class options : MonoBehaviour
{
    public GameObject slowShip;
    public GameObject fastShip;
    public GameObject enabledCheats;
    public GameObject disabledCheats;
    public bool cheat;

    public GameManager gameManager;

    public void Start()
    {
        slowShip.SetActive(false);
        fastShip.SetActive(false);
        enabledCheats.SetActive(false);
        disabledCheats.SetActive(true);
        cheat = false;

        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);

    }

    /// <summary>
    /// Player chooses default ship, slow bullets
    /// </summary>
    public void slow()
    {
        slowShip.SetActive(true);
        fastShip.SetActive(false);
        //code goes here
        gameManager.defaultShip = true;
    }


    /// <summary>
    /// Player chooses second ship, fast bullets
    /// </summary>
    public void fast()
    {
        slowShip.SetActive(false);
        fastShip.SetActive(true);
        //code goes here
        gameManager.defaultShip = false;
    }

    public void cheats()
    {
        //code here
        if (cheat == false)
        {
            //Debug.Log("Cheats is false turn true"); 
            enabledCheats.SetActive(true);
            disabledCheats.SetActive(false);
            cheat = true;
            gameManager.cheatmode = true;
        }
        else if (cheat == true)
        {
            //Debug.Log("Cheats is true turn false");
            enabledCheats.SetActive(false);
            disabledCheats.SetActive(true);
            cheat = false;
            gameManager.cheatmode = false;
        }

    }
}
