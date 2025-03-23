using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the game
using UnityEngine.UI; // For using the health bar UI element

public class PlayerCollision : MonoBehaviour
{
    public int maxHits = 2; // Player can get hit twice before game over
    private int currentHits = 0;
    public Image healthBar;  // Reference to the single UI Image (health bar)
    private Rigidbody2D rb;

    // Width range of the health bar
    public float minHealthBarWidth = 0f; // Minimum width (0 hits)
    public float maxHealthBarWidth = 90f; // Maximum width (full health)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on player!");
        }

        UpdateHealthBar();  // Initialize the health bar
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("idk")) // Check if the collided object is an enemy
        {
            Destroy(other.gameObject);
            currentHits++; // Increase hit count
            Debug.Log("Player hit! Hits taken: " + currentHits);

            // Decrease health bar by 1 hit (each hit reduces by 1)
            UpdateHealthBar();

            // If health drops to 0 or below, restart the game
            if (currentHits >= maxHits)
            {
                EndGame();
            }
        }
        else if (other.CompareTag("Heal")) // Check if the collided object is a healing object
        {
            Destroy(other.gameObject);
            // Decrease hit count (healing player by 1 hit)
            if (currentHits > 0)
            {
                currentHits--;
            }

            Debug.Log("Player healed! Hits remaining: " + (maxHits - currentHits));

            // Update health bar
            UpdateHealthBar();
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart game
    }

    // Function to update the health bar width (sizeDelta changes)
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Calculate the width based on the current number of hits
            float healthBarWidth = Mathf.Lerp(minHealthBarWidth, maxHealthBarWidth, (maxHits - currentHits) / (float)maxHits);

            // Update the health bar width by changing the sizeDelta of the RectTransform
            RectTransform healthBarRect = healthBar.GetComponent<RectTransform>();
            healthBarRect.sizeDelta = new Vector2(healthBarWidth, healthBarRect.sizeDelta.y);
        }
        else
        {
            Debug.LogWarning("Health bar UI element is missing!");
        }
    }
}