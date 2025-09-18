using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //on start save where our object spawns into, what position its in
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //calling the rotate in place function off for right now
        //RotateInPlace();
        PingPong();

    }

    void RotateInPlace()
    {

        transform.Rotate(0, 90 * Time.deltaTime, 0, Space.Self);
    }

    void PingPong()
    {
        //making local variables to store the direction, distance and speed of movement
        Vector3 moveDir = Vector3.right;
        float moveDistance = 2f;
        float moveSpeed = .5f;

        //t goes from 0 to 1 back to 0 over time
        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
        Vector3 offset = moveDir.normalized * (t * moveDistance);
        //final position is the start plus the offset
        transform.position = startPos + offset;
    }
}
