using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField][Min(1)] float scrollSpeed = 2f;

    List<Transform> layers;
    int layerCount;

    // Start is called before the first frame update
    void Start()
    {
        layers = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            layers.Add(transform.GetChild(i));
        }
        layerCount = layers.Count;
    }

    private void Update()
    {
        for (int layerIndex = 0; layerIndex < layerCount; layerIndex++)
        {
            float parallaxMultiplier = (float)(layerCount - layerIndex) / layerCount;
            layers[layerIndex].transform.localPosition -= Vector3.right * parallaxMultiplier * scrollSpeed * Time.deltaTime;
        }
    }
}
