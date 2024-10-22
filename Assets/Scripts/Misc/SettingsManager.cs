using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject SettingsCanvas;
    private bool isVisible;

    void Start() 
    {
        isVisible = false;
        SettingsCanvas.SetActive(false);
    }

    void Update()
    {
        if(Input.GetButtonDown("Settings")) ToggleSettings();
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void ToggleSettings()
    {
        if(!isVisible)
        {
            SettingsCanvas.SetActive(true);
            isVisible = true;
        }
        else 
        {
            SettingsCanvas.SetActive(false);
            isVisible = false;
        }
    }
}
