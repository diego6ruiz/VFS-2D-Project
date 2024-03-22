using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int currentScore = 0;
    public static int collectibleCount;

    // Optional: If you have a UI element to display the score
    public TMPro.TextMeshProUGUI scoreText; // Assign in the Inspector
    public GameObject finalText;

    public void Start()
    {
        collectibleCount = GameObject.FindGameObjectsWithTag("Collectible").Length;
        finalText.SetActive(false);
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        // Update the UI if you have one
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }

        collectibleCount--;

        if (collectibleCount <= 0)
        {
            AllCollectiblesCollected();
        }
    }

    void AllCollectiblesCollected()
    {
        // All collectibles have been collected
        Debug.Log("All collectibles collected!");
        finalText.SetActive(true);
    }
}
