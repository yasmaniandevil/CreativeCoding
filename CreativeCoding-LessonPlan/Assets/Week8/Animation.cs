using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator animator;
    public GameObject animationObject;
    // Start is called before the first frame update
    void Start()
    {
        animator = animationObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        animator.SetTrigger("PlayAnimation");
        Debug.Log("played animation");
    }
}
