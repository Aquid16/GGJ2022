using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] float yPos = 3;
    [SerializeField] float transitionDuration = .5f;

    int side = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transform.position = new Vector3(0, yPos, -10);
    }

    public void ChangePos()
    {
        side *= -1;
        transform.DOMoveY(side * yPos, transitionDuration);
    }
}
