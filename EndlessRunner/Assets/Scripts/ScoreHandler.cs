using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler instance;

    private void Awake()
    {
        instance = this;
    }

    public int score { get; private set; }

    // Update is called once per frame
    public void UpdateScore(int amount)
    {
        score += amount;
        UIManager.instance.UpdateScoreText();
    }
}
