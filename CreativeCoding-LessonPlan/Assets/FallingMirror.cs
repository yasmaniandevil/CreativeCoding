using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMirror : MonoBehaviour
{
    public List<Rigidbody> rigidBodyMirrors = new List<Rigidbody>();
    public float waitTime = 2f;

    //this is a coroutine which means a function that can be paused (time)
    IEnumerator FallingMirrors()
    {
        //for every rigidbody on our list
        foreach (Rigidbody rb in rigidBodyMirrors)
        {
            //turn off kinematic
            rb.isKinematic = false;
            //wait 2 seconds between them
            yield return new WaitForSeconds(waitTime);
            
        }
        
    }

    //when we enter the trigger start the coroutine
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FallingMirrors());
            
        }
    }
}
