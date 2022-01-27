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

    ObjectPooler pooler;
    float prevXSpawn;

    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        StartCoroutine(SpawnObstacles());
    }

    void SpawnNextObstacle()
    {
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPoint = new Vector2(prevXSpawn + distance, 0);
        GameObject obst = pooler.GetPooledItem();
        obst.transform.localPosition = spawnPoint;
        obst.SetActive(true);
        prevXSpawn += distance;
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            SpawnNextObstacle();
        }
    }
}
