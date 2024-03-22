using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    public GameObject projectilePrefab;
    public float shootInterval = 2f; // Time between shots
    public float colorChangeDuration = 0.2f;

    private float timeSinceLastShot = 0f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= shootInterval)
        {
            timeSinceLastShot = 0f; // Reset timer
            ChangeColor(Color.red); // Change color before shooting
            ShootProjectile();
            Invoke("ResetColor", colorChangeDuration); // Change back later
        }
    }

    void ShootProjectile()
    {
        // Make sure the player exists before trying to shoot at them
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Give the projectile a velocity
            projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f; // Adjust speed as needed
        }
    }

    void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }
}
