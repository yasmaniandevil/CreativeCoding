using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //player movement variables
    public float speed = 5f;
    private Rigidbody2D rb2d;
    public float jumpForce = 4f;
    private Vector2 moveInput;

    //ground check variables
    public LayerMask groundLayer;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //grab the rigidbody2d off the player and store it inside our var
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * speed, rb2d.linearVelocity.y);
    }

    //move function that we are going to call on our player input!
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 horizontalInput = context.ReadValue<Vector2>();
        moveInput = new Vector2(horizontalInput.x, 0f);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //if i am hitting the correct key and i am grounded
        //take our isgrounded if you do not care if they are grounded
        if (context.performed && isGrounded())
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    //this function has no void which means it returns something, in this case a bool
    //this is optional, if you dont want your player to be able to jump forever
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    //this is just helping us see our ground check radius, pure visualization also optional
    private void OnDrawGizmosSelected()
    {
        //if we dont have that little feet check on our player we are going to exit the function
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("any collision");

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //subtract our health
            Debug.Log("player looses health");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpeedPickup"))
        {
            speed += 5;
            Destroy(collision.gameObject);
        }
    }
    
}
