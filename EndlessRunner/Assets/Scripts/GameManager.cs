using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool firstInput { get; set; } = true;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public float gameSpeed = 3f;
    float speedSetting;

    private void Start()
    {
        speedSetting = gameSpeed;
        gameSpeed = 0f;
    }

    public void StartGame()
    {
        gameSpeed = speedSetting;
        firstInput = false;
    }
}
