using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip audio;
    private float volume;
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if it isnt the player
        if (!other.gameObject.CompareTag("Player")) return;
        //if it is currently playing
        if (audioSource.isPlaying) return;
        //if it is the player
        if (other.CompareTag(("Player")))
        {
            //play the audio
            audioSource.Play();
            Debug.Log("Played Audio");
            
        }
       
    }
}
