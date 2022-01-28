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

    public float gameSpeed = 3f;

    public bool passedTutorial { get; set; }

    bool isPaused;
    bool gameRunning;
    float speedSetting;

    int tutorialCount;
    int tutorialMax = 6;

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
        StartCoroutine(RaiseSpeed());
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

    public void StopGame()
    {
        gameSpeed = 0;
        gameRunning = false;
    }

    IEnumerator RaiseSpeed()
    {
        gameRunning = true;
        while (gameRunning)
        {
            gameSpeed += 0.05f * Time.deltaTime;
            yield return null;
        }
    }

    public float GetRelativeDistance()
    {
        return gameSpeed / speedSetting;
    }

    public void IncrementTutorialCount()
    {
        tutorialCount++;
        if (tutorialCount >= tutorialMax)
        {
            passedTutorial = true;
            UIManager.instance.EnableScoreText();
        }
    }
}
