using System.Collections;
using UnityEngine;
using TMPro;
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
    private PlayerController mainPlayer;
    public GameObject enemy;

    // HUD
    public TextMeshProUGUI healthText;
    public Text objectiveText;

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
        healthText.text = "Health Remaining: " + mainPlayer.health;
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Routine To Avoid Projectiles Spam
    private IEnumerator Timer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(.01f);
            int playerHealth = mainPlayer.health;

            if (playerHealth <= 0)
            {
                yield return new WaitForSeconds(2);
                GameOver();
            }
            else if (mainPlayer.win)
            {
                Win();
            } 
            // Change the health displayed if needed and make projectiles throwable
            else
            {
                healthText.text = "Health Remaining: " + playerHealth;
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

    private IEnumerator FadeInObjective(float time, Text objective)
    {
        objective.color = new Color(objective.color.r, objective.color.g, objective.color.b, 0);
        while(objective.color.a < 1f)
        {
            objective.color = new Color(objective.color.r, objective.color.g, objective.color.b, objective.color.a + (Time.deltaTime / time));
        }
        yield return null;
    }

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
