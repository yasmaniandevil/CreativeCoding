using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //if it is not the player exit the function here
        if (!other.gameObject.CompareTag("Player")) return;
        //if the audio is currently playing then exit the function here
        if (audioSource.isPlaying) return;

        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            Debug.Log("Played Audio");
        }
    }
}
