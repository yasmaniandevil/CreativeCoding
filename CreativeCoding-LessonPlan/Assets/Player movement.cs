using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermovement : MonoBehaviour
{
    //rigid is a component that gives our game object physics
    private Rigidbody rb;
    //determine how fast we move
    public float walkSpeed = 5;
    private float currentSpeed;
    
    private bool isRunning;
    public float runningSpeed = 10f;
    public float jumpForce = 5f;
    // how fast our camera look around
    public float cameraLookSpeed = 4;
    public Transform cameraTransform;

    private float yRotation = 0;
    private float xRotation = 0;
    public GameObject SpherePrefab;
    //audio source variable  to grab audio
    public AudioSource _SFX;
    
    
    //spawn variables
    //how many obstacles are spawning
    public int numberOfObstacles = 10;
    //how far are the obstacles from each other, their spacing
    private float spacing = 3f;
    //what prefab are we spawning
    public GameObject obstaclePrefab;
    public GameObject coinPrefab; // coin prefab
    //public score1 score1Script;
    public Transform respawnPoint;
    bool isDestroyed = false;
    private int maxJumps = 5;

    private int jumpCount;
    public bool isGrounded;

    private List<GameObject> reloadObjs = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed; // 正确初始化成员变量
        SpawnObjects();
        SpawnCoins();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // 使用成员变量 currentSpeed
        rb.velocity = transform.forward * zInput * currentSpeed +
                      transform.right * horizontalInput * currentSpeed +
                      Vector3.up * rb.velocity.y;

        CameraLook();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpCount < maxJumps)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            Debug.Log("jump");
        }

        // Shift 跑步
        isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runningSpeed : walkSpeed;

        // 玩家掉落重置
        if (transform.position.y < -8)
        {
            Respawn();
            

        }
    }


    void CameraLook()
    {
        //we first make a variable called mouseX
        //then we assign it to our mouse input and multiplied it iby our camera is looking
        float mouseX = Input.GetAxis("Mouse X") *  cameraLookSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraLookSpeed * Time.deltaTime;
        
        //when we move the mouse horizontally, we rotate around the xaxis to look the left and right
        
        yRotation += mouseX;
        //rotate the player left/right on the y axis rotation
        transform.rotation = Quaternion.Euler(0f, yRotation, 0);
        
        //decrease x rotation when moving mouse up so camera tilts up
        // increase x rotation when moving camera down so it tilts down
        xRotation -= mouseY;
        //clamping it so can doesnt rotate forever prevents flipping
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        
        
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            float zPos = i * spacing;

            // 障碍物左右交错
            float xPos = (i % 2 == 0) ? -1.5f : 1.5f;
            Vector3 obstaclePos = new Vector3(xPos, 1.5f, zPos);
            Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);

        }

        
        
    }

//two object hits each other in unity, unity then make a report and tells you what object you hit
//by saving it in collision variable
//for a collision or trigger to happen both objects
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        jumpCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("sfx1"))
        {
            _SFX.Play();
            Debug.Log("play audio");
        }
        //Debug.Log("I triggered: " + other.gameObject.name);
        //if the other game object i hit has the tag trigger on it
        if (other.CompareTag("trigger"))
        {
            
            //Debug.Log("I triggered: " + other.gameObject.name);
            //we first accessed the script then we called the function
            //score1Script.AddScore(1);
            //Destroy(other.gameObject);
            other.gameObject.SetActive((false));
            isDestroyed = true;
            
            
            //SpawnObjects();
            //SpawnCoins();

        }
    }
//we call this function, if our player falls, 
    private void Respawn()
    {
        //the transform.position references the transform this sript is on
        //the script is on the player, so it's the position of the position
        //we are setting the player position to the respawn position
        transform.position = respawnPoint.position;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Die()
    {
        currentSpeed = 0;
        rb.velocity = Vector3.zero;
        isRunning = false;
        //Debug.Log("Player Died!");
        Respawn();
    }

    void SpawnCoins()
    {
        for (int i = 0; i <= 10; i++)
        {
            
            // Coin 放在障碍物的相反位置
            if (coinPrefab != null)
            {
                float xPos = (i % 2 == 0) ? -1.5f : 1.5f;
                float coinX = -xPos; // 左右相反
                float zPos = i * spacing;
                Vector3 coinPos = new Vector3(coinX, 1.5f, zPos);
                Instantiate(coinPrefab, coinPos, Quaternion.identity);
            }
        }
    }
}
