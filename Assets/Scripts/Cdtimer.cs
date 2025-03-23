using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 300f; // 5 minutes in seconds (300 seconds)
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;  // Reference to a TextMeshProUGUI component that will display the timer

    void Start()
    {
        // Start the timer
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;  // Decrease time by the time passed since the last frame
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;  // Stop the timer when it reaches 0
                // You can add a game over or any other behavior here
            }
        }

        // Display the time on the screen
        DisplayTime(timeRemaining);
    }

    // Format and display the time in minutes:seconds format
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  // Get minutes
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);  // Get remaining seconds

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);  // Update the UI text with the timer
    }
}
