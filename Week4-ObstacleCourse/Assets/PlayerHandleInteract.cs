using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHandleInteract : MonoBehaviour
{
    public Image reticle;
    public List<GameObject> collectedObjects = new List<GameObject>();
    private bool changeScene = false;

    // Update is called once per frame
    void Update()
    {
        Interact();
        CheckObjects();
    }

    void Interact()
    {
        //grab reticle image and change the color
        reticle.GetComponent<Image>().color = Color.red;

        //shoot ray for reticle
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //shoot our ray at 5 meters
        if (Physics.Raycast(ray, out hit, 5))
        {
            //if our object has an interactable tag
            if (hit.collider.CompareTag("Interactable"))
            {
                //then we change the reticle to black
                reticle.GetComponent<Image>().color = Color.black;
                Debug.Log("I Hit: " + hit.collider.gameObject.name);

                //if we press our left mouse button down
                if (Input.GetMouseButtonDown(0))
                {
                    //add what we clicked to our list
                    collectedObjects.Add(hit.collider.gameObject);
                    //turn off that game object
                    hit.collider.gameObject.SetActive(false);
                    Debug.Log("collected: " + collectedObjects.Count);
                }
            }
        }
    }

    private void CheckObjects()
    {
        //if we have three things on our list
        if (collectedObjects.Count == 3)
        {
            //then if change scene is true (you have walked through the trigger)
            //then we can switch scenes
            if (changeScene)
            {
                //then we can change scenes or play a sound or whatever
                SceneManager.LoadScene(1);
                
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if we walk into the trigger than leads us to our next scene
        //turn our bool true
        if (other.gameObject.CompareTag("ChangeScene"))
        {
            changeScene = true;
        }
    }
}
