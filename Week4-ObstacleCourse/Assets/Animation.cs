using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator _animationClip;
    
    //this is for if the animator is on a different object that the script
    //we first need to tell unity what object
    public GameObject animationObj;
    
    //we want an empty string to be a "stand in" for the name of our trigger that we are actually going to put in the inspector
    public string animationTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //we are grabbing the animator off the object 
        //if the animator is on the same obj as the script then you do not need the game object and can just write getcomponenet
        //this is bc unity assumes the animator is on the same obj the script is
        _animationClip = animationObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //you can test it out first on an input press
        if (Input.GetKey(KeyCode.C))
        {
            _animationClip.SetTrigger(animationTrigger);
            Debug.Log("played animation");
        }
    }

    void OnTriggerEnter(Collider other)
    {//if the player collides with this object
        if (other.CompareTag("Player"))
        {
            //play our animaton
            //you can also just write your trigger name in directly with "" (quotes) 
            //but by leaving it blank and using a stand in, we can put this script on multiple game objects and just write whatever in the inspector
            _animationClip.SetTrigger(animationTrigger);
            Debug.Log("triggered animation");
        }
    }
}
