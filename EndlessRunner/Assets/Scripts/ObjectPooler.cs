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
            //if (item.prefab.tag.Contains("Hell"))
            //{
            //    tagsHell.Add(item.prefab.tag);
            //}
            //else if (item.prefab.tag.Contains("Heaven"))
            //{
            //    tagsHeaven.Add(item.prefab.tag);
            //}
        }
    }

    public GameObject GetPooledItem()
    {
        //List<string> tags = new List<string>();
        //tags.AddRange(PlayerController.instance.GetSide() == 1 ? tagsHeaven : tagsHell);
        int index = Random.Range(0, _tags.Count);
        for(int i=0; i<pooledObjects.Count-1; i++)
        {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(_tags[index]))
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
