using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] text;
    public bool start;
    public bool quit;
    public bool coop;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (start)
        {
            SceneManager.LoadScene(1);
        }
        
        if (coop)
        {
            SceneManager.LoadScene(2);
        }

        if (quit)
        {
            Application.Quit();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(text[0]);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(text[1]);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(text[2]);
        }
    }

    
}
