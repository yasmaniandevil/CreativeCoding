using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HandleInteractions : MonoBehaviour
{

    public Image reticle;
    public List<GameObject> collectedObjects = new List<GameObject>(); 
    public bool changeScene = false;
    public int sceneIndex = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Interact();
        
        if (collectedObjects.Count >= 2)
        {
            if (changeScene)
            {
                switch (sceneIndex)
                {
                    case 0:
                        SceneManager.LoadScene(0);
                        break;
                    case 1:
                        SceneManager.LoadScene(1);
                        break;
                    case 2:
                        SceneManager.LoadScene(2);
                        break;
                    case 3:
                        SceneManager.LoadScene(3);
                        break;
                }
                
                changeScene = false;
            }
        }

        Debug.Log(sceneIndex);
        
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
                //Debug.Log("I Hit: " + hit.collider.gameObject.name);
                
                if (Input.GetMouseButtonDown(0))
                {
                    collectedObjects.Add(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                    //Debug.Log("collected" + hit.collider.gameObject.name);
                }
                
            }

        }
    }

    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FallCube"))
        {
            changeScene = true;
            sceneIndex = 2;
        }
        
        if (other.gameObject.CompareTag("SpringCube"))
        {
            changeScene = true;
            sceneIndex = 3;
        }

        if (other.gameObject.CompareTag("WinterCube"))
        {
            changeScene = true;
            sceneIndex = 1;
        }
        
        if (other.gameObject.CompareTag("SummerCube"))
        {
            changeScene = true;
            sceneIndex = 0;
        }
    }

  
    
}
