using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 1f;

    public float forceAmt = 10f;
    Rigidbody rb;

    //for pingpong
    Vector3 startPos;

    /*1. rigidbody movement
     2. fixed update
    3. Move Object function
    4.Rotate
    5. PingPong*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //for the ping pong function
        //store the position
        startPos = transform.position;
        Debug.Log("StartPos: " + startPos);
        Debug.Log("TrnasformPos: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //RotateInPlace();

        PingPong();
    }

    private void FixedUpdate()
    {
        // Pushes forward with a force, every physics step
        //requries a rigidbody
        //if(rb == null) return;
        //rb.AddForce(Vector3.forward * forceAmt);
    }

    public void MoveObject()
    {
        //manual control of position
        //no physics just moving it frame by frame
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void RotateInPlace()
    {
        float degrees = 90;
        transform.Rotate(0f, degrees * Time.deltaTime, 0f, Space.Self);
    }

    public void PingPong()
    {
        //move it right and have it up
        Vector3 moveDir = Vector3.right;
        float moveDistance = 2f;
        float moveSpeed = .5f;

        //t goes from 0 to 1 and back to 0 over time
        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
        //concert 0 or 1 to a distance along the chosen direction
        Vector3 offset = moveDir.normalized * (t * moveDistance);
        //final position is the start plus the offset
        transform.position = startPos + offset;
    }

    
}
