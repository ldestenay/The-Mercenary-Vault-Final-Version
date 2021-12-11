using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Screens
    public GameObject titleScreen;
    public GameObject room;
    public GameObject creditsScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;

    // 3D Objects
    public GameObject enemy;

    // HUD
    public Text objectiveText;
    public Text bossText;
    public Image h1;
    public Image h2;
    public Image h3;


    // Button Start Pressed
    public void StartGame()
    {
        // Display Labyrinth
        titleScreen.SetActive(false);
        room.SetActive(true);

        // Display objective
        StartCoroutine(FadeInObjective(1f, objectiveText));
        StartCoroutine(FadeOutObjective(1f, objectiveText));
    }

    // Button Credits Pressed
    public void ShowCredits()
    {
        // Display Credits Screen
        titleScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    // Button ExitCredits Pressed
    public void ExitCredits()
    {
        // Display Back Title Screen
        creditsScreen.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Display the GameOver sreen
    public void GameOver()
    {
        room.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    // Display the win screen
    public void Win()
    {
        new WaitForSeconds(3);
        room.SetActive(false);
        winScreen.SetActive(true);
    }

    /// <summary>
    /// Routine showing the objective on the screen
    /// </summary>
    /// <param name="time"></param>
    /// <param name="objective"></param>
    /// <returns></returns>
    public IEnumerator FadeInObjective(float time, Text objective)
    {
        objective.color = new Color(objective.color.r, objective.color.g, objective.color.b, 0);
        while(objective.color.a < 1f)
        {
            objective.color = new Color(objective.color.r, objective.color.g, objective.color.b, objective.color.a + (Time.deltaTime / time));
        }
        yield return null;
    }

    /// <summary>
    /// Routine removing the objective of the screen
    /// </summary>
    /// <param name="time"></param>
    /// <param name="objective"></param>
    /// <returns></returns>
    public IEnumerator FadeOutObjective(float time, Text objective)
    {
        yield return new WaitForSeconds(2);
        objective.color = new Color(objective.color.r, objective.color.g, objective.color.b, 1);
        while (objective.color.a > 0f)
        {
            objective.color = new Color(objective.color.r, objective.color.g, objective.color.b, objective.color.a - (Time.deltaTime / time));
        }
    }
}
