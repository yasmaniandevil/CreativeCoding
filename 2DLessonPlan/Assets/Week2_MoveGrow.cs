using UnityEngine;

public class Week2_MoveGrow : MonoBehaviour
{
    //first we make a transform variable
    private Transform triangleTransform;
    public float moveSpeed = 3;
    private int direction = 1;

    public int minBoundary = -8;
    public int maxBoundary = 8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //then we assign our empty var to what we will grab
        triangleTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //easy rotate
        //triangleTransform.Rotate(0, 0, 90);
        //fancier rotation
        //can make a rotation speed variable
        triangleTransform.Rotate(Vector3.forward * 20 * Time.deltaTime);

        //new vector 3( x, y, x) i want it to move on the x
        triangleTransform.localPosition += new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0);

        if(triangleTransform.position.x < minBoundary ||  triangleTransform.position.x > maxBoundary)
        {
            //Debug.Log("hit boundary");
            direction *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //triangleTransform.localScale = new Vector3(3, 3, 3);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit player");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Hit bullet");
            Destroy(gameObject);
        }
        Debug.Log("collided with obj");
    }
}

