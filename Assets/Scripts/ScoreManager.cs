using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI updateScorePrefab;

    public static ScoreManager instance;
    
    private void Awake()
    {
        instance = this;
        UpdateMaxItemAmount();
    }
    
    private void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        Scoring.EscapismScore += 20;
        UpdateScoreText();
        CreateScoreUpdateNotification("+");
        UpdateMaxItemAmount();
    }

    public void DecreaseScore()
    {
        Scoring.EscapismScore -= 5;

        if (Scoring.EscapismScore <= 0)
        {
            StartCoroutine(GameManager.instance.GameOver());
        }
        else
        {
            UpdateScoreText();
            CreateScoreUpdateNotification("-");
            UpdateMaxItemAmount();
        }
    }

    public void IncreaseMood()
    {
        Scoring.MoodScore += 15;
        Debug.Log(Scoring.MoodScore);
    }

    public void DecreaseMood()
    {
        Scoring.MoodScore -= 10;
    }
    
    void UpdateScoreText()
    {
        scoreText.text = Scoring.EscapismScore.ToString();
    }

    void CreateScoreUpdateNotification(string points)
    {
        updateScorePrefab.text = points;
        TextMeshProUGUI tempText = Instantiate(updateScorePrefab, updateScorePrefab.transform.position, Quaternion.identity);
        tempText.transform.SetParent(canvas.transform, false);
        
        Destroy(tempText.gameObject, 2f);
    }

    void UpdateMaxItemAmount()
    {
        if (Scoring.EscapismScore > 59 && Scoring.EscapismScore <= 79)
        {
            Scoring.MaxItemAmount = 40;
        }
        else if (Scoring.EscapismScore > 79 && Scoring.EscapismScore <= 89)
        {
            Scoring.MaxItemAmount = 30;
        }
        else if (Scoring.EscapismScore > 89)
        {
            Scoring.MaxItemAmount = 18;
        }
    }
}
