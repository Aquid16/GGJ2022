using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public GameObject prefab;
        public int number;
    }

    [SerializeField] Transform objectsParent;
    [SerializeField] List<ItemData> poolData;

    List<GameObject> pooledObjects = new List<GameObject>();
    List<string> tagsHell = new List<string>();
    List<string> tagsHeaven = new List<string>();

    List<string> _tags = new List<string>();

    [SerializeField] List<string> tutorialSpawns;
    int tutorialSpawnCount;

    int hellSpawnConsecutive;
    int heavenSpawnConsecutive;

    [SerializeField] string chosenTag;

    private void Start()
    {
        foreach (ItemData item in poolData)
        {
            for(int i=0;i<item.number;i++)
            {
                GameObject inst = Instantiate(item.prefab, objectsParent);
                inst.SetActive(false);
                pooledObjects.Add(inst);
            }

            _tags.Add(item.prefab.tag);
        }
    }

    public GameObject GetPooledItem()
    {
        string pulledTag = string.Empty;
        int index = 0;
        if (tutorialSpawnCount < tutorialSpawns.Count)
        {
            pulledTag = tutorialSpawns[tutorialSpawnCount];
            tutorialSpawnCount++;
        }
        else
        {
            index = Random.Range(0, _tags.Count);
            chosenTag = _tags[index];
            if (index > 2)
            {
                heavenSpawnConsecutive = 0;
                hellSpawnConsecutive++;
                if (hellSpawnConsecutive >= 3)
                {
                    index = Random.Range(0, 3);
                    hellSpawnConsecutive = 0;
                    heavenSpawnConsecutive++;
                }
            }
            else if (index < 3)
            {
                hellSpawnConsecutive = 0;
                heavenSpawnConsecutive++;
                if (heavenSpawnConsecutive >= 3)
                {
                    index = Random.Range(3, _tags.Count);
                    heavenSpawnConsecutive = 0;
                    heavenSpawnConsecutive++;
                }
            }
            pulledTag = _tags[index];
        }
        
        for(int i=0; i<pooledObjects.Count-1; i++)
        {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(pulledTag))
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
