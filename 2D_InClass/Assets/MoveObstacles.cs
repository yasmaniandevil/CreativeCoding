using UnityEngine;
using UnityEngine.AI;

public class MoveObstacles : MonoBehaviour
{
    public float moveSpeed = 3f;
    private int direction = 1;
    public int minBoundary = -8;
    public int maxBoundary = 8;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        //easy simple rotation
        transform.Rotate(Vector3.forward * 20 * Time.deltaTime);

        //we are only moving it on the x, on the y and z it is 0, 0
        transform.localPosition += new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0);
        //if the triangle goes past the boundary in either direction, flip the triangles direction
        if(transform.position.x < minBoundary || transform.position.x > maxBoundary)
        {
            direction *= -1;
        }
    }
}
