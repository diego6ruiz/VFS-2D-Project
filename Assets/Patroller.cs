using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Enemy
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

            // Determine the direction to the current waypoint
            Vector3 scale = transform.localScale;
            if (transform.position.x < targetPosition.x) // Target is to the right
            {
                scale.x = Mathf.Abs(scale.x); // Ensure scale.x is positive
            }
            else if (transform.position.x > targetPosition.x) // Target is to the left
            {
                scale.x = -Mathf.Abs(scale.x); // Ensure scale.x is negative
            }
            transform.localScale = scale;

            // Check if reached the waypoint
            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Cycle waypoints
            }
        }
    }
}
