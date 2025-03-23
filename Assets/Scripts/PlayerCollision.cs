using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the game

public class PlayerCollision : MonoBehaviour
{
    public int maxHits = 2; // Player can get hit twice before game over
    private int currentHits = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("idk")) // Check if the collided object is an enemy
        {
            GameObject enemyInstance = other.gameObject; // Store reference to the instance
            Destroy(enemyInstance); // Destroy only this specific enemy
            currentHits++; // Increase hit count
            Debug.Log("Player hit! Hits taken: " + currentHits);

            if (currentHits >= maxHits)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart game
    }
}



 