using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public static ScoreManager instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        Scoring.EscapismScore += 20;
        UpdateScoreText();
    }

    public void DecreaseScore()
    {
        Scoring.EscapismScore -= 5;

        if (Scoring.EscapismScore <= 0)
        {
            GameManager.instance.GameOver();
        }
        else
        {
            UpdateScoreText();
        }
    }
    
    void UpdateScoreText()
    {
        scoreText.text = Scoring.EscapismScore.ToString();
    }
}
