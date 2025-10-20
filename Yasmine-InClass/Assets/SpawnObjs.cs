using UnityEngine;

public class SpawnObjs : MonoBehaviour
{
    //spawning variables
    //how many obstacles are spawning
    public int numberOfObstacles = 10;
    //how far are the obstacles from each other, their spacing
    private float spacing = 3f;
    //what prefab are we spawning
    public GameObject obstaclePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnObjects()
    {
        //this is simple instantiation
        //Instantiate(spherePrefab, new Vector3(2, 3, 1), Quaternion.identity);

        //first we make a variable called i and we set it to 0
        //if i is less than the number of obstacles then 
        // instantiate or do whatever is inside the brackets
        //i + 1 = i++, i continues to count up until it hits 10 then it stops
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 position = new Vector3(i * spacing, 1.5f, 0);
            Instantiate(obstaclePrefab, position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnObjects();
            Debug.Log("SpawnedObjs");
        }
    }
}
