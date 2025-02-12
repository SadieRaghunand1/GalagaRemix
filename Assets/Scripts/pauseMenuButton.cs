using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuButton : MonoBehaviour
{
    public GameObject playImage;
    private bool menu;
    // opens the how to play section while paused
    public void howPlay()
    {
        if (menu == false)
        {
            playImage.SetActive(false);
            menu = true;
        }
        else if (menu == true)
        {
            playImage.SetActive(true);
            menu = false;
        }
    }
    public void Start()
    {
        playImage.SetActive(false);
        menu = false;
    }
}
