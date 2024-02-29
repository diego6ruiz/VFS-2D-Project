using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject heartPrefab; // Assign in the inspector
    public Transform heartsContainer; // Assign in the inspector
    private List<GameObject> hearts = new List<GameObject>();

    // Method to update the hearts display to match current number of lives
    public void SetLives(int lives)
    {
        // Add hearts if there are too few
        while (hearts.Count < lives)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            hearts.Add(newHeart);
        }

        // Remove hearts if there are too many
        while (hearts.Count > lives)
        {
            GameObject toRemove = hearts[hearts.Count - 1];
            hearts.Remove(toRemove);
            Destroy(toRemove);
        }
    }
}
