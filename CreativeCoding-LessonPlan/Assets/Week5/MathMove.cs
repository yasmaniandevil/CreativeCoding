using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathMove : MonoBehaviour
{
    private Rigidbody rb;

    public float forceAmt = 7;

    public enum Functions
    {
        MathMoveSin,
        MathMoveCos,
        AddForces,
        MathLerp
        
    }
    public Functions functionsToPerform;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //MathMoveSin();
        //MathMoveCos();
        //MathLerp();
        //AddForces();

        switch (functionsToPerform)
        {
            case Functions.MathMoveSin:
                MathMoveSin();
                break;
            case Functions.MathMoveCos:
                MathMoveCos();
                break;
            case Functions.MathLerp:
                MathLerp();
                break; 
            case Functions.AddForces:
                AddForces();
                break;
            
        }
    }

    public void MathMoveSin()
    {
        //natural smooth movement
        float y = Mathf.Sin(Time.time * 2) * 0.5f;
        transform.position = new Vector3(transform.position.x, y + 1.5f, transform.position.z);
    }

    void MathMoveCos()
    {
        float z = Mathf.Cos(Time.time * 2) * 0.5f;
        transform.position = new Vector3(transform.position.x, transform.position.y, z + 1.5f);
        
    }

    void MathLerp()
    {
        Vector3 pointA = new Vector3(3, 1.5f, 3);
        Vector3 pointB = new Vector3(6, 1.5f, 6);
        //goes from 0 to 1 to back to 0
        float t = Mathf.PingPong(Time.time, 1);
        transform.position = Vector3.Lerp(pointA, pointB, t);
    }

    void AddForces()
    {
        // Pushes forward with a force, every physics step
        //requries a rigidbody
        /*ForceMode.Force Adds a gradual push spread over time.
        Feels like acceleration (car slowly speeding up).*/
        //forcemode.impulse Adds an instant push, ignoring mass and acceleration.
        // Feels like a sudden kick or jump.
        /*Force: wind pushing the player, conveyor belt, rocket engines.
        Impulse: jump, gun recoil, explosion knockback.*/
        if(rb == null) return;
        rb.AddForce(Vector3.up * forceAmt, ForceMode.Force);
    }
}
