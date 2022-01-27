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

    [SerializeField] List<ItemData> poolData;
    List<GameObject> pooledObjects = new List<GameObject>();
    private void Start()
    {
        foreach (ItemData item in poolData)
        {
           for(int i=0;i<item.number;i++)
            {
                GameObject inst = Instantiate(item.prefab);
                inst.SetActive(false);
                pooledObjects.Add(inst);
            }
        }
    }

    public GameObject GetPooledItem()
    {
        for(int i=0; i<pooledObjects.Count-1; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
