using System.Collections;
using UnityEngine;
using TMPro;

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
}
