using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjs : MonoBehaviour
{
    private int numberOfObstacles = 4;
    public GameObject obstaclePrefab;
    private float spacing = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SpawnObjects()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 position = new Vector3(i * spacing, 1.5f, 0);
            Instantiate(obstaclePrefab, position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnObjects();
            Debug.Log("spawned objs");
        }
    }
}
