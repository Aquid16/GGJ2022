using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    List<Transform> layers;
    List<float> initialXPositions;
    int layerCount;

    // Start is called before the first frame update
    void Start()
    {
        layers = new List<Transform>();
        initialXPositions = new List<float>();
        for (int i = 0; i < transform.childCount; i++)
        {
            layers.Add(transform.GetChild(i));
            initialXPositions.Add(layers[i].position.x);
        }
        layerCount = layers.Count;
    }

    private void Update()
    {
        for (int layerIndex = 0; layerIndex < layerCount; layerIndex++)
        {
            float parallaxMultiplier = (float)(layerCount - layerIndex) / layerCount;
            Transform layer = layers[layerIndex];
            layer.localPosition -= Vector3.right * parallaxMultiplier * GameManager.instance.gameSpeed * Time.deltaTime;
            if (layer.position.x <= -18 * layer.localScale.x + initialXPositions[layerIndex])
            {
                layer.localPosition = new Vector3(initialXPositions[layerIndex], layer.localPosition.y);
            }
        }
    }
}
