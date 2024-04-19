using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    MuseumLoadSystem LoadSystem;

    private void Start()
    {
        
        GameObject gameObject = GameObject.Find("VisitorManager");
        if (gameObject!= null)
            LoadSystem = gameObject.GetComponent<MuseumLoadSystem>();
    }

    public void ExitMuseum()
    {
        LoadSystem.LoadScene("MainMenu", "MuseumVisitor");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
