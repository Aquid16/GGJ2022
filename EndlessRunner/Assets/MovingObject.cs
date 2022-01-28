using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.anchoredPosition -= GameManager.instance.gameSpeed * Time.deltaTime * Vector2.right;
        if (rect.anchoredPosition.x <= -12)
        {
            gameObject.SetActive(false);
        }
    }
}
