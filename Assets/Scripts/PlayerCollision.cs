using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the game

public class PlayerCollision : MonoBehaviour
{
    public int maxHits = 2; // Player can get hit twice before game over
    private int currentHits = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on player!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("idk")) // Check if the collided object is an enemy
        {
            GameObject enemyInstance = other.gameObject; // Store reference to the instance
            Destroy(enemyInstance); // Destroy only this specific enemy
            currentHits++; // Increase hit count
            Debug.Log("Player hit! Hits taken: " + currentHits);

            if (currentHits == 1)
            {
                DeleteHealthBar_g();
            }

            if (currentHits == 2)
            {
                DeleteHealthBar_y();
            }

            if (currentHits == 3)
            {
                DeleteHealthBar_o();
            }

            if (currentHits >= maxHits)
            {
                DeleteHealthBar_r();
                EndGame();
            }
        }
        else if (other.CompareTag("Wall"))
        {
            rb.linearVelocity = Vector2.zero;
            Debug.Log("Player stopped at the wall.");
        }
        else if (other.CompareTag("Heal"))
        {


            GameObject enemyInstance = other.gameObject; // Store reference to the instance
            Destroy(enemyInstance); // Destroy only this specific enemy
            if (currentHits > 0)
            {
                currentHits--;
            }
         // Increase hit count



            if (currentHits == 1)
            {
                DeleteHealthBar_g();
            }

            if (currentHits == 2)
            {
                DeleteHealthBar_y();
            }

            if (currentHits == 3)
            {
                DeleteHealthBar_o();
            }
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart game
    }

    void DeleteHealthBar_g()
    {
        GameObject greenhealthBar = GameObject.FindWithTag("healthbar_g");
        if (greenhealthBar != null)
        {
            Destroy(greenhealthBar);
            Debug.Log("Health bar object deleted!");
        }
        else
        {
            Debug.Log("No object with tag 'healthbar' found.");
        }
    }

    void DeleteHealthBar_y()
    {
        GameObject yellowhealthBar = GameObject.FindWithTag("healthbar_y");
        if (yellowhealthBar != null)
        {
            Destroy(yellowhealthBar);
            Debug.Log("Health bar object deleted!");
        }
        else
        {
            Debug.Log("No object with tag 'healthbar' found.");
        }
    }

    void DeleteHealthBar_o()
    {
        GameObject orangehealthBar = GameObject.FindWithTag("healthbar_o");
        if (orangehealthBar != null)
        {
            Destroy(orangehealthBar);
            Debug.Log("Health bar object deleted!");
        }
        else
        {
            Debug.Log("No object with tag 'healthbar' found.");
        }
    }

    void DeleteHealthBar_r()
    {
        GameObject redhealthBar = GameObject.FindWithTag("healthbar_r");
        if (redhealthBar != null)
        {
            Destroy(redhealthBar);
            Debug.Log("Health bar object deleted!");
        }
        else
        {
            Debug.Log("No object with tag 'healthbar' found.");
        }
    }
}



 