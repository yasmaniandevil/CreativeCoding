using System;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator _animationClip;
    public GameObject animationObj;
    public string animationTrigger;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animationClip = animationObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            _animationClip.SetTrigger(animationTrigger);
            Debug.Log("played animation");
            
            
            
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animationClip.SetTrigger(animationTrigger);
            Debug.Log("triggered animation");
        }
    }

    
}
