using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleActive()
    {
        if (image.activeInHierarchy)
        {
            image.SetActive(false);
            Debug.Log("image is true");
        }
        else
        {
            image.SetActive(true);
            Debug.Log("image is false");    
        }
    }
}
