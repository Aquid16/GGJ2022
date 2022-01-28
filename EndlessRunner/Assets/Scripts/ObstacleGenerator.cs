using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
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
        StartCoroutine(SpawnObstacles());
    }

    void SpawnNextObstacle()
    {
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPoint = new Vector2(prevXSpawn + distance, 0);
        GameObject obst = pooler.GetPooledItem();
        float side = Mathf.Sign(Random.Range(-2, 2));
        obst.transform.localPosition = spawnPoint;
        obst.transform.localScale = new Vector3(1, side, 1);
        obst.GetComponentInChildren<SpriteRenderer>().color = side >= 1 ? Color.black : Color.white;
        obst.SetActive(true);
        prevXSpawn += distance;
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
            SpawnNextObstacle();
        }
    }
}
