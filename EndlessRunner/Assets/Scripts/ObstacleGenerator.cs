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
    [Space]
    [SerializeField] RectTransform flipTutorialCanvas;

    ObjectPooler pooler;
    bool spawning;
    bool firstGiantSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();
    }

    public void StartSpawning()
    {
        spawning = true;
        StartCoroutine(SpawnObstacles());
    }

    void SpawnNextObstacle()
    {
        float distance = Random.Range(minSpawnDistance * GameManager.instance.GetRelativeDistance(),
            maxSpawnDistance * GameManager.instance.GetRelativeDistance());
        Vector2 spawnPoint = new Vector2(distance, 0);
        GameObject obst = pooler.GetPooledItem();
        obst.transform.localPosition = spawnPoint;
        obst.SetActive(true);
        if (obst.CompareTag("Heaven Giant") && firstGiantSpawn)
        {
            flipTutorialCanvas.anchoredPosition = new Vector2(obst.transform.position.x - 7.5f, -1f);
            flipTutorialCanvas.gameObject.SetActive(true);
            firstGiantSpawn = false;
        }
    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(3f);
        while (spawning)
        {
            SpawnNextObstacle();
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
        }
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
