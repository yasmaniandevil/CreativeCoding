using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleInteractions : MonoBehaviour
{

    public Image reticle;
    public List<GameObject> collectedObjects = new List<GameObject>(); 
    private bool changeScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
        CheckObjects();
    }

   
    
    public void Interact()
    {
        // Reticle
        reticle.GetComponent<Image>().color = Color.red;

        //shoot ray for reticle
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        //Shoots ray at 5 meters
        if (Physics.Raycast(ray, out hit, 5))
        {
            //if object has interactable tag
            if (hit.collider.CompareTag("Interactable"))
            {
                //turn reticle black
                reticle.GetComponent<Image>().color = Color.black;
                Debug.Log("I Hit: " + hit.collider.gameObject.name);
                
                if (Input.GetMouseButtonDown(0))
                {
                    collectedObjects.Add(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                    Debug.Log("collected" + hit.collider.gameObject.name);
                }
                
            }

        }
    }

    public void CheckObjects()
    {
        if (collectedObjects.Count >= 2)
        {
            if (changeScene)
            {
                Debug.Log("collected objects");
                SceneManager.LoadScene(1);
            }
            
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ChangeScene"))
        {
            changeScene = true;
        }
    }
}
