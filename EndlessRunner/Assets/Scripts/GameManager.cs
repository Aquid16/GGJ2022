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
    bool isPaused;
    float speedSetting;

    private void Start()
    {
        Time.timeScale = 1;
        speedSetting = gameSpeed;
        gameSpeed = 0f;
    }

    public void StartGame()
    {
        gameSpeed = speedSetting;
        ObstacleGenerator.instance.StartSpawning();
        firstInput = false;
    }

    public void TogglePause()
    {
        if (UIManager.instance.subWindowOpen) return;
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        UIManager.instance.DisplayPauseScreen(isPaused, 0.75f);
    }
}
