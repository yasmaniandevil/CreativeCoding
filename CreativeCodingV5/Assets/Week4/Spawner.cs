using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spacing = 3;
    public int numberOfObstacles;
    
    public List<GameObject> listOfPrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnObjectsList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObjectsList()
    {
        
        for (int i = 0; i < listOfPrefabs.Count; i++)
        {
            //choose a random prefab from the list
            int randomPrefab = Random.Range(0, listOfPrefabs.Count);
            GameObject prefabToSpawn = listOfPrefabs[randomPrefab];

            // Work out the position for this obstacle
            // Example: (0, 0, 0), then (3, 0, 0), then (6, 0, 0), etc.
            Vector3 spawnPosition = transform.position + new Vector3(i * spacing, 1.5f, 0f);

            //instantiates random prefab at random position
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            Debug.Log("spawn list of objs");
        }
    }
}
