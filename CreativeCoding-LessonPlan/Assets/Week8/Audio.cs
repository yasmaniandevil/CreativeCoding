using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip audio;
    private float volume;
    public AudioSource audioSource;
    private bool hasPlayed = false;
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
        if (!other.gameObject.CompareTag("Player")) return;
        if (hasPlayed) return;
        
        if (other.CompareTag(("Player")))
        {
            audioSource.Play();
            Debug.Log("Played Audio");
            hasPlayed = true;
        }
       
    }
}
