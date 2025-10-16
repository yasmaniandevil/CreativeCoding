using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    //movement variables
    //rigidbody is a component that gives our game object physics
    //such as gravity and mass
    private Rigidbody rb;
    //this is just a float that determines how fast we move
    public float walkSpeed = 5;
    private float currentSpeed;
    public bool isRunning;
    public float runningSpeed = 10f;
    public float jumpForce = 5f;

    //camera variables
    //var for how fast our camera looks around
    public float cameraLookSpeed;
    //the transform of our camera
    public Transform cameraTransform;
    private float yRotation = 0;
    private float xRotation = 0;
    public GameObject spherePrefab;

    //we are making a reference to our score script so we can call our function
    public Score scoreScript;

    //this is going to be an empty game object with a transform that places our player here
    public Transform respawnPoint;

    public int maxJumps = 5;
    private int jumpCount;
    public bool isGrounded;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //when player starts game current speed is the walk speed
        currentSpeed = walkSpeed;
        
        jumpCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //this is unitys input system
        //horizontal is mapped to the left and right arrow keys and a/d
        float horizontalInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //this line is what makes us actually walk
        rb.linearVelocity = transform.forward * zInput * currentSpeed + transform.right *
        horizontalInput * currentSpeed + Vector3.up * rb.linearVelocity.y;

        CameraLook();


        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jump");
            jumpCount++;
           
        }

        //if we press the shift key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Hit shift");
            //our bool turns to true
            isRunning = true;
        }
        else
        {
            //when the shift key is not pressed our bool is false
            isRunning = false;
        }

        //if isRunning equals true (the player is pressing shift)
        if (isRunning)
        {
            //change the current speed to running speed
            currentSpeed = runningSpeed;
            Debug.Log("change speed");
        }
        else
        {
            //if it is not true than change current speed to walk speed
            currentSpeed = walkSpeed;
        }

        //Debug.Log("Current Speed is: " + currentSpeed);
        //if our player falls we call our respawn function that resets the position of the player
        if(transform.position.y < -8)
        {
            Respawn();
        }

    }

    void CameraLook()
    {
        //first made a variable called mouseX
        //then we assigned it to our mouse input 
        //multiplied it by how fast our camera is looking 
        float mouseX = Input.GetAxis("Mouse X") * cameraLookSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraLookSpeed * Time.deltaTime;

        //when we move the mouse horizontally
        //we rotate around the y axis to look left and right
        yRotation += mouseX;
        //rotate the the player left/right on y axis rotation
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        //decrease xRotation when moving mouse up so camera tilts up
        //increase x rotation when moving camera down so it tilts down
        xRotation -= mouseY;
        //clamping it so cam doesnt rotate forever prevents flipping
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    

    //two objects hit each other in unity
    //unity then makes a report and tells you what obj you hit
    //by saving it in the collision variable
    //for a collision or trigger to happen BOTH objects need a box collider
    //and one of them needs a rigidbody
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I Hit: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            
        }
        
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        jumpCount = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        

        Debug.Log("I Triggered: " + other.gameObject.name);

        //if the other game object i hit has the tag trigger on it
        if (other.CompareTag("Trigger"))
        {
            Debug.Log("found trigger");
            //SpawnObjects();
            //we first accessed the script then we called the function
            scoreScript.AddScore(1);
        }

       
    }

    //we call this function if our player falls
    private void Respawn()
    {
        //the transform.position references the transform this scipt is on
        //the script is on the player so its the position of the player
        //we are setting the players position to the respawn position
        transform.position = respawnPoint.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
