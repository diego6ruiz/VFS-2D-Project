using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold waypoint positions
    public float speed = 3f;
    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (waypoints.Length > 0)
        {
            // Move towards the current waypoint
            Vector2 targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if reached the waypoint
            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Cycle waypoints
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("HOLA");
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Make the player a child of the platform
            //collision.gameObject.transform.parent = transform;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("ADIOS");
        // When the player leaves the platform, remove the parent-child relationship
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); // Remove the player from being a child of the platform
            //collision.gameObject.transform.parent = null;
        }
    }
}
