using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    private float lastMoveHorizontal;
    private bool isWalking = true;

    public Vector2 spawnPoint;

    public float leftBound;
    public float rightBound;
    public float detectionRange = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 patrolDirection = Vector2.left;


    public Transform mainPlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastMoveHorizontal = -0.01f;
        spawnPoint = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, mainPlayerPosition.position);
        if (distanceToPlayer <= detectionRange)
        {
            Attack();
        }
        else
        {
            Patrol();
        }


        animator.SetBool("isWalking", isWalking);
        animator.SetFloat("moveX", lastMoveHorizontal);

    }

    void Patrol()
    {
        if(transform.position.x < leftBound)
        {
            patrolDirection = Vector2.right;
        } else if(transform.position.x > rightBound)
        {
            patrolDirection = Vector2.left;
        }
        Move(patrolDirection.x);
    }

    void Attack()
    {
        float direction = mainPlayerPosition.position.x - transform.position.x;
        Move(direction);
        //check OnCollisionEnter2D 

    }

    void Move(float horizontalInput)
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        lastMoveHorizontal = Mathf.Sign(horizontalInput); 
        isWalking = horizontalInput != 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (playerController.isAttacking)
                {
                    Destroy(gameObject);
                }
                else
                {
                    playerController.lives--; // Decrement the player's lives
                    playerController.uiManager.SetLives(playerController.lives); // Update the UI

                    if (playerController.lives <= 0)
                    {
                        EditorApplication.isPlaying = false;
                    }
                    else
                    {
                        Vector2 respawnPoint = new Vector2(-0.75f, 2.73f);
                        playerController.transform.position = respawnPoint;
                        // Optionally, add logic for player invulnerability time, respawn, etc.
                    }
                }
            }
        }
    }
}
