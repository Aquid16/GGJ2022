using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public static ObstacleGenerator instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Spawn Parameters")]
    [SerializeField] [Min(1)] float minSpawnDistance = 4f;
    [SerializeField] [Min(1)] float maxSpawnDistance = 6f;
    [SerializeField] [Min(1)] float minSpawnRate = 1f;
    [SerializeField] [Min(1)] float maxSpawnRate = 2f;

    ObjectPooler pooler;
    float prevXSpawn;

    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObstacles());
    }

    void SpawnNextObstacle()
    {
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPoint = new Vector2(distance, 0);
        GameObject obst = pooler.GetPooledItem();
        obst.transform.localPosition = spawnPoint;
        obst.SetActive(true);
        prevXSpawn += distance;
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnNextObstacle();
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
        }
    }

    public void StopSpawning()
    {
        StopCoroutine(SpawnObstacles());
    }
}
