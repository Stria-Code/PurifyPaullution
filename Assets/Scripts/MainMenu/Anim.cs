using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public CanvasGroup mainMenuGroup;

    private bool fadeIn = false;
    private bool fadeOut = false;

    public GameObject optionsPanel;
    Animation creditsAnim;

    // Start is called before the first frame update
    void Start()
    {
        //optionsPanel = GameObject.Find("Panel");
        creditsAnim = optionsPanel.GetComponent<Animation>();

       // mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        mainMenuGroup = mainMenuCanvas.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if(mainMenuGroup.alpha < 1)
            {
                mainMenuGroup.alpha += Time.deltaTime;
                if (mainMenuGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if(mainMenuGroup.alpha >= 0)
            {
                mainMenuGroup.alpha -= Time.deltaTime;
                if(mainMenuGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public void ShowCredits()
    {
        HideMainMenu();
        creditsAnim.Play("ShowCredits");
    }

    public void ShowMainMenu()
    {
        fadeIn = true;
    }

    public void HideMainMenu()
    {
        fadeOut = true;
    }

    public void HideCredits()
    {
        ShowMainMenu();
        creditsAnim.Play("HideCredits");
    }
}
