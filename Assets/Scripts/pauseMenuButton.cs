using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseMenuButton : MonoBehaviour
{
    public GameObject playImage;
    public bool menu;
    public Canvas canvas;
    // opens the how to play section while paused
    public void howPlay()
    {
        
        if (menu == false)
        {
            playImage.SetActive(true);
            menu = true;
        }
        else if (menu == true)
        {
            playImage.SetActive(false);
            menu = false;
        }
    }


    public void ShowHowToPlay()
    {
        Debug.Log("Show how to play");
        //playImage.SetActive(true);
        canvas.enabled = true;
        menu = true;
    }

    public void HideHowToPlay()
    {
        Debug.Log("Hide to play");
        //playImage.SetActive(false);
        canvas.enabled = false;
        menu = false;
    }

    public void Start()
    {
       // playImage.SetActive(false);
        menu = false;
    }
}
