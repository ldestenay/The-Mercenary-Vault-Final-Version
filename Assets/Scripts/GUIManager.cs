using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject titleMenu;
    public GameObject characterMenu;
    public GameObject creditsMenu;

    public void StartGame()
    {
        titleMenu.SetActive(false);
        characterMenu.SetActive(true);
    }

    public void ShowCredits()
    {
        titleMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ExitCredits()
    {
        titleMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
}