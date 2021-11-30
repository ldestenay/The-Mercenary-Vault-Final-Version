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


    // Position of the spawn of the player when pressing start
    private readonly Vector3 startPosition = new Vector3(0, 0.75f, 0);

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
            // Can throw projectiles every 0.3 seconds
            yield return new WaitForSeconds(0.3f);

            if(mainPlayer.health <= 0)
            {
                GameOver();
            }
            else if (mainPlayer.win)
            {
                Win();
            } 
            // Change the health displayed if needed and make projectiles throwable
            else
            {
                healthText.text = "Health Remaining: " + mainPlayer.health;
                mainPlayer.throwableProjectiles = true;
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
