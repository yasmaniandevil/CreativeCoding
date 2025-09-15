using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float lookSpeed;

    public Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        /*input manager is a unity system that maps
         physical input to name "axes" or action,
        it comes with default mappings. horizontal is a
        default mapping, mapped to a/d left/right
        and joystick x-axis
        think of it as a translator
        w-> input manager says vertical = +1
        A input manager says "Horizontal = -1*/
        
        //this is our WASD
        float x = Input.GetAxis("Horizontal"); //assigns a/d key and left and right arrow
        float z = Input.GetAxis("Vertical"); //assigns w/s key and up and down arrow
        //no y because we move left to right(x) and forward to back(z)


        //make a new variable called y bc that is our up and down
        float y = 0;
        //press E to go up positive direction
        /*if (Input.GetKey(KeyCode.E))
        {
            y = 1;
        }

        //press F to go down
        if (Input.GetKey(KeyCode.F))
        {
            y = -1;
        }*/

        //combine the x, y, z into one direction vector
        Vector3 move = new Vector3(x, y, z);

        //multiply by speed and frame time -> smooth consistent movement
        move = move * moveSpeed * Time.deltaTime;

        //actually move the camera (local space - relative to where the camera is facing
        transform.Translate(move);

        //mouse look
        //mouse movement on the x axis (left/right)
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;

        //movement on the y axis (up and down) negative so it feels natural
        float mouseY = -Input.GetAxis("Mouse Y") * lookSpeed;

        //rotate around the world y axis (turn left/right)
        transform.Rotate(0, mouseX, 0, Space.World);

        //rotate around the cameras local x axis (up and down)
        transform.Rotate(mouseY, 0, 0, Space.Self);

    }
}
