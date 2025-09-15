using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        else
        {
            isRunning = false;
        }

        
        if (isRunning)
        {
            currentSpeed = runningSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        //Debug.Log("current speed is: " + currentSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jump");
        }

        Death();
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

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("hit the obstacle");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            Debug.Log("hit the trigger");
            SpawnObjects();

            if(scoreScript != null)
            {
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
