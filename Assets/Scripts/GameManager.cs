using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Screens
    public GameObject titleScreen;
    public GameObject room;
    public GameObject creditsScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;

    // 3D Objects
    private PlayerController mainPlayer;
    public GameObject enemy;

    // HUD
    public Text objectiveText;
    public Image h1;
    public Image h2;
    public Image h3;

    // Is Game Over
    private bool isGameOver = false;


    // Button Start Pressed
    public void StartGame()
    {
        // Display Labyrinth
        titleScreen.SetActive(false);
        room.SetActive(true);

        // Game Setter
        mainPlayer = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(Timer());

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

    // Routine To Avoid Projectiles Spam
    private IEnumerator Timer()
    {
        while (!isGameOver)
        {
            yield return null;
            int playerHealth = mainPlayer.health;

            if (playerHealth <= 0)
            {
                h1.enabled = false;
                h2.enabled = false;
                h3.enabled = false;
                yield return new WaitForSeconds(2);
                GameOver();
            }
            else if (mainPlayer.win)
            {
                Win();
            } 
            // Change the health displayed
            else
            {
                switch (mainPlayer.health)
                {
                    case 3:
                        h1.enabled = true;
                        h2.enabled = true;
                        h3.enabled = true;
                        break;
                    case 2:
                        h1.enabled = true;
                        h2.enabled = true;
                        h3.enabled = false;
                        break;
                    case 1:
                        h1.enabled = true;
                        h2.enabled = false;
                        h3.enabled = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Display the GameOver sreen
    private void GameOver()
    {
        isGameOver = true;
        room.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    // Display the win screen
    private void Win()
    {
        isGameOver = true;
        room.SetActive(false);
        winScreen.SetActive(true);
    }

    /// <summary>
    /// Routine showing the objective on the screen
    /// </summary>
    /// <param name="time"></param>
    /// <param name="objective"></param>
    /// <returns></returns>
    private IEnumerator FadeInObjective(float time, Text objective)
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
    private IEnumerator FadeOutObjective(float time, Text objective)
    {
        yield return new WaitForSeconds(2);
        objective.color = new Color(objective.color.r, objective.color.g, objective.color.b, 1);
        while (objective.color.a > 0f)
        {
            objective.color = new Color(objective.color.r, objective.color.g, objective.color.b, objective.color.a - (Time.deltaTime / time));
        }
    }
}
