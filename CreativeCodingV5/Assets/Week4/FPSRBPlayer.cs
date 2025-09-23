using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSRBPlayer : MonoBehaviour
{
    private Rigidbody rb;
    public float walkSpeed = 5f;

    //how fast is our camera moving
    public float lookSpeed;
    //we need a ref to our camera
    public Transform cameraTransform;
    //tracking camera vertical and horizontal movement 
    private float yRotation = 0;
    private float xRotation = 0;

    float currentSpeed;
    public bool isRunning;
    public float runningSpeed;
    public float jumpForce = 5f;

    //script reference
    public Score scoreScript;

    //spawning var
    public int numberOfObstacles = 10;
    private float spacing = 3f;
    public GameObject obstaclePrefab;

    //respawn point
    public Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnObjects();
        
        rb = GetComponent<Rigidbody>();

        //we lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //set current speed to walk speed
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //read keyboard input for movement (WASD)
        float horizontalInput = Input.GetAxis("Horizontal");
        //zInput gets players w or s input which is -1 or 1
        float zInput = Input.GetAxis("Vertical");

        //transform.forward (0, 0, 1)
        //transform.forward is the direction the player is facing
        //transform.right   is the local right direction (X+)
        rb.velocity = transform.forward * zInput * currentSpeed +
            transform.right * horizontalInput * currentSpeed + Vector3.up * rb.velocity.y;

        CameraLook();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            currentSpeed = runningSpeed;
        }
        else
        {
            isRunning = false;
            currentSpeed = walkSpeed;
        }

        //Debug.Log("current speed is: " + currentSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jump");
        }

        Death();

        //restart button
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void CameraLook()
    {
        //getting and assiging mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;

        //when the mouse moves horizontally
        //we rotate around the y axis to look left and right
        yRotation += mouseX;
        //rotate the player left/right on y axis rotation
        transform.rotation = Quaternion.Euler(0f, yRotation, 0);

        //decrease xRotation when moving mouse up so camera tilts up
        //increase x rotation when moving cam down so cam tilts down
        xRotation -= mouseY;
        //clamp the camera (pitch) so we can not flip over
        xRotation = Mathf.Clamp(xRotation, -90, 90); //prevents flipping

        //apply local rotation to the camera only
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);

    }

    //think of it as a report
    //when we hit an object
    //unity will store what object we hit in our collision var
    //then it passes it in to our method
    //games rely on collisions and triggers
    //for a collision or trigger to happen both obj need a collider and one needs a rigidbody
    private void OnCollisionEnter(Collision collision)
    {
        //first do normal collision without tag
        Debug.Log("I Hit: " + collision.gameObject.name);
        
        //what if we want to collide with a specific game object
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("I Hit: " + collision.gameObject.name);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //first do normal trigger
        Debug.Log("I Triggered: " + other.gameObject.name);
        
        //but what if we want to trigger a specifc game obj
        if (other.CompareTag("Trigger"))
        {
            Debug.Log("I Triggered: " + other.gameObject.name);
            SpawnObjects();
            
            //if the score script exists
            if(scoreScript != null)
            {
                //call the function add score
                scoreScript.AddScore(1);

            }

            Destroy(other.gameObject);
        }
    }

    public void SpawnObjects()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 position = new Vector3(i * spacing, 1.5f, 0);
            Instantiate(obstaclePrefab, position, Quaternion.identity);
        }
    }

    public void Death()
    {
        //if the player falls below y = -5 resets them to the respawn point
        if(transform.position.y < -5)
        {
            
            //if it exists
            if(respawnPoint != null)
            {
                //transform.position means the transform of the gameobject the script is on
                transform.position = respawnPoint.position;

            }
            else
            {
                //fallback
                transform.position = Vector3.zero;
            }
        }
    }


}
