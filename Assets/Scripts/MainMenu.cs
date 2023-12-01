using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool start;
    public bool quit;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (start)
        {
            SceneManager.LoadScene(1);
        }

        if (quit)
        {
            Application.Quit();
        }
    }
}
