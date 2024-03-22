using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100; // Example 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(50); // Example damage, adjust as needed
            Destroy(collision.gameObject); // Destroy the weapon
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            DealDamageToPlayer(collision);
        }
    }

    private void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            // Check if the object has a parent
            if (transform.parent != null)
            {
                // If there's a parent, destroy the parent object
                Destroy(transform.parent.gameObject);
            }
            else
            {
                // If there's no parent, destroy this game object
                Destroy(gameObject);
            }
        }
    }


    private void DealDamageToPlayer(Collider2D collision)
    {
        // You'll need a reference to your player's health script here:
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)  // Safety check
        {
            playerHealth.TakeDamage(10); // Example damage 
        }
    }
}
