using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    //we have to give it the image we want it to turn on and off
    public GameObject image;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //this is how we would call a function
        //and we have to pass in the scene name
        //if i press the E key then load the scene
        //LoadScene(SampleScene);
        
    }

    //we wrote a function to call when we want to change a scene
    //this function takes in an argument which is our string
    //and passes it in to load our scene
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

//all functions need brackets
//bc inbetween the brackets is the task that it is performing
    public void ToggleImage()
    {
        //if the image is active in the hierarchy we set it to false (off) when clicking button
        if(image.activeInHierarchy)
        {
            image.SetActive(false);
        }else //else if the image is NOT active in the heirarchy
        //we set it to true, and turn it on!
        {
            image.SetActive(true);
        }

    }
}
