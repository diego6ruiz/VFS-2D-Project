using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public int maxJumps = 2;
    private int jumpsLeft;
    private bool isGrounded;
    private float lastMoveHorizontal;

    public Vector2 respawnPoint;
    public float deathThreshold = -10f; 

    public int lives = 3;

    public bool isAttacking = false;

    private Rigidbody2D rb;
    private Animator animator;
    public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpsLeft = maxJumps;
        lastMoveHorizontal = 0;
        respawnPoint = transform.position;
        uiManager.SetLives(lives);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        animator.SetFloat("moveX", moveHorizontal);

        if (moveHorizontal != 0)
        {
            lastMoveHorizontal = moveHorizontal;
            animator.SetBool("isWalking", true);
        }
        
        else 
        {
            animator.SetBool("isWalking", false);
        }

        animator.SetFloat("moveX", lastMoveHorizontal);

        if(Input.GetButtonDown("Jump"))
        {
            if(isGrounded || jumpsLeft > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                if (!isGrounded)
                {
                    jumpsLeft--;
                }

                animator.SetBool("isJumping", true);

                if(isGrounded)
                {
                    isGrounded = false;
                    jumpsLeft = maxJumps - 1;
                }
            }            
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PerformAttack());
        }

        if(transform.position.y < deathThreshold)
        {
            transform.position = respawnPoint;
            rb.velocity = Vector2.zero;
            jumpsLeft = maxJumps;
            animator.SetBool("isJumping", false);

            lives--;
            uiManager.SetLives(lives); 
            if(lives == 0)
            {
                GameOver();
            }
        }

        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Win"))
        {
            Debug.Log("YOU WIN");Debug.Log("YOU WIN");
        }        
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        EditorApplication.isPlaying = false;

    }

    IEnumerator PerformAttack()
    {
        Debug.Log("Attacking");
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);

        yield return new WaitForSeconds(0.8f); // Attack duration

        Debug.Log("NOT Attacking");
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }

}
