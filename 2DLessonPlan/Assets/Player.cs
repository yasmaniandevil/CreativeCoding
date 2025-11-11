using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;
    public float jumpForce = 5;

    private Vector2 moveInput;

    public LayerMask groundLayer;
    public float groundRadius = .15f;
    public Transform groundCheck;
    
    public TextMeshProUGUI healthText;
    public int health = 100;
    public TextMeshProUGUI gameOverText;
    
    //shooting var
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        UpdateHealthText();
        gameOverText.gameObject.SetActive(false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //bool ground = isGrounded();
        //Debug.Log(ground);

        if (health <= 0)
        {
            health = 0;
            //Debug.Log("Game Over");
            gameOverText.gameObject.SetActive(true);
        }
        
        //Debug.Log("speed is" + speed);
        Shoot();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        
        Vector2 horizontalInput = context.ReadValue<Vector2>();
        moveInput = new Vector2(horizontalInput.x, 0f);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        
    }
    
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            health--;
            UpdateHealthText();
            Destroy(collision.gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpeedPickup"))
        {
            speed += 5;
        }
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
