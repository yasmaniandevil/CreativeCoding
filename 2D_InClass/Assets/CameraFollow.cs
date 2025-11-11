using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private float smoothSpeed = 125f;
    private Vector3 offset = new Vector3(0, 0, -10);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame
    void LateUpdate()
    {
        //if we assigned a player in the inspector
        if(player != null)
        {
            //create a new position that considers where the cam is relative to the player
            Vector3 desiredPos = player.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
            transform.position = smoothPos;
        }
    }
}
