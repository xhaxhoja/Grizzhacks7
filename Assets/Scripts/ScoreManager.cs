using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the Inspector!");
            return;
        }
        UpdateScoreText();
    }

    void Update()
    {
        // Example: Increase score over time
        score += 1;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}