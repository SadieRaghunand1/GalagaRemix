using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class options : MonoBehaviour
{
    public GameObject slowShip;
    public GameObject fastShip;

    public void Start()
    {
        slowShip.SetActive(false);
        fastShip.SetActive(false);
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
    }
}
