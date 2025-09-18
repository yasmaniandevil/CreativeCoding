using UnityEngine;

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

    //spawning variables
    //how many obstacles are spawning
    public int numberOfObstacles = 10;
    //how far are the obstacles from each other, their spacing
    private float spacing = 3f;
    //what prefab are we spawning
    public GameObject obstaclePrefab;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //when player starts game current speed is the walk speed
        currentSpeed = walkSpeed;

        SpawnObjects();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jump");
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

    private void SpawnObjects()
    {
        //this is simple instantiation
        //Instantiate(spherePrefab, new Vector3(2, 3, 1), Quaternion.identity);

        //first we make a variable called i and we set it to 0
        //if i is less than the number of obstacles then 
        // instantiate or do whatever is inside the brackets
        //i + 1 = i++, i continues to count up until it hits 10 then it stops
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 position = new Vector3(i * spacing, 1.5f, 0);
            Instantiate(spherePrefab, position, Quaternion.identity);
        }
    }
}
