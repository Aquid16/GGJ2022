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
    int lockState;
    int spawnWhileLocked;
    int consecutiveHeavenSpawns;
    int consecutiveHellSpawns;

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

            //_tags.Add(item.prefab.tag);
            if (item.prefab.tag.Contains("Heaven"))
            {
                tagsHeaven.Add(item.prefab.tag);
            }
            else if (item.prefab.tag.Contains("Hell"))
            {
                tagsHell.Add(item.prefab.tag);
            }
        }
    }

    public GameObject GetPooledItem()
    {
        string pulledTag;
        
        if (tutorialSpawnCount < tutorialSpawns.Count)
        {
            pulledTag = tutorialSpawns[tutorialSpawnCount];
            tutorialSpawnCount++;
        }
        else
        {
            pulledTag = ChooseObjectTag();
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

    string ChooseObjectTag()
    {
        string chosen;

        if (lockState == 1)
        {
            chosen = tagsHell[Random.Range(0, 2)];
            spawnWhileLocked++;
            if (spawnWhileLocked >= 3)
            {
                lockState = spawnWhileLocked = 0;
                consecutiveHellSpawns = 2;
            }

            return chosen;
        }

        if (consecutiveHeavenSpawns > 2)
        {
            chosen = tagsHell[Random.Range(0, tagsHell.Count)];
            consecutiveHellSpawns++;
            consecutiveHeavenSpawns = 0;
        }
        else if (consecutiveHellSpawns > 2)
        {
            int index = Random.Range(0, tagsHeaven.Count);
            chosen = tagsHeaven[Random.Range(0, tagsHeaven.Count)];
            consecutiveHeavenSpawns++;
            consecutiveHellSpawns = 0;

            if (index == 3)
            {
                lockState = 1;
            }
        }
        else
        {
            int minChoose = 0, maxChoose = tagsHeaven.Count + tagsHell.Count;
            int index = Random.Range(minChoose, maxChoose);
            if (index >= tagsHeaven.Count)
            {
                chosen = tagsHell[index - tagsHeaven.Count];
                consecutiveHellSpawns++;
                consecutiveHeavenSpawns = 0;
            }
            else
            {
                chosen = tagsHeaven[index];

                if (index == 3)
                {
                    lockState = 1;
                }
            }
        }


        return chosen;
        //if (index > 3)
        //{
        //    heavenSpawnConsecutive = 0;
        //    hellSpawnConsecutive++;
        //    if (hellSpawnConsecutive >= 3)
        //    {
        //        index = Random.Range(0, 4);
        //        hellSpawnConsecutive = 0;
        //        heavenSpawnConsecutive++;
        //    }
        //}
        //else if (index < 4)
        //{
        //    hellSpawnConsecutive = 0;
        //    heavenSpawnConsecutive++;
        //    if (heavenSpawnConsecutive >= 3)
        //    {
        //        index = Random.Range(4, _tags.Count);
        //        heavenSpawnConsecutive = 0;
        //        heavenSpawnConsecutive++;
        //    }
        //}
    }






    // 0 through 3- Heaven
    // 4 through 6- Hell

    //if we're spawn locked to hell only, choose hell and diregard consecutive
    //magically count 3 spawns then lift the lock

    //If heaven chosen, check how many consecutive heaven obstacles we spawned
    //if it's 2 or more, choose from hell.
    //Else, check if we're spawning that damned Giant train.
    //if we're not, increment consecutive heaven spawns and return the tag
    //if we are, lock spawns to Hell only.
}
