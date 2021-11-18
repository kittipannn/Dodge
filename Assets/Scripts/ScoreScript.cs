using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private float currentScore = 0;
    private int score = 0;
    public int Score { get => score; }

    private int highScore;
    public int HighScore { get => highScore; }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore");
    }

    private void OnEnable()
    {
        highScore = PlayerPrefs.GetInt("highscore");
    }
    void Update()
    {
        countScore();
    }
    void countScore() 
    {
        currentScore += Time.deltaTime;
        score = Mathf.RoundToInt(currentScore);
    }

    public void saveHighscore() 
    {
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }
}
