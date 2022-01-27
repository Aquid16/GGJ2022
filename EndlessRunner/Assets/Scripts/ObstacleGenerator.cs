using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform obstacleParent;
    [Space]
    [Header("Spawn Parameters")]
    [SerializeField] float minSpawnDistance = 4f;
    [SerializeField] float maxSpawnDistance = 6f;

    float prevXSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SpawnNextObstacle()
    {
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        GameObject obst = Instantiate(obstaclePrefab, new Vector3(prevXSpawn + distance, 0), Quaternion.identity, obstacleParent);

    }
}
