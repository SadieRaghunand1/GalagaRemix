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

    public void Start()
    {
        slowShip.SetActive(false);
        fastShip.SetActive(false);
        enabledCheats.SetActive(false);
        disabledCheats.SetActive(true);
        cheat = false;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);

    }

    public void slow()
    {
        slowShip.SetActive(true);
        fastShip.SetActive(false);
        //code goes here
    }

    public void fast()
    {
        slowShip.SetActive(false);
        fastShip.SetActive(true);
        //code goes here
    }

    public void cheats()
    {
        //code here
        if (cheat == false)
        {
            enabledCheats.SetActive(true);
            disabledCheats.SetActive(false);
            cheat = true;
        }
        if (cheat == true)
        {
            enabledCheats.SetActive(false);
            disabledCheats.SetActive(true);
            cheat = false;
        }

    }
}
