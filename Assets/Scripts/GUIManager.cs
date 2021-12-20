using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public GameObject titleMenu;
    public GameObject creditsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1Scene");
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
