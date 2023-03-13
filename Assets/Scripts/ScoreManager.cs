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
    public int MaxItemAmount { get; private set; } = 50;
    
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
        CreateScoreUpdateNotification("+ 20");
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
            CreateScoreUpdateNotification("- 5");
        }
    }
    
    void UpdateScoreText()
    {
        scoreText.text = Scoring.EscapismScore.ToString();
    }

    void CreateScoreUpdateNotification(string points)
    {
        updateScorePrefab.text = points;
        TextMeshProUGUI tempText = Instantiate(updateScorePrefab, new Vector2(), Quaternion.identity);
        tempText.transform.SetParent(canvas.transform, false);
        
        Destroy(tempText.gameObject, 2f);
    }

    void UpdateMaxItemAmount()
    {
        //IMPLEMENT!!!
    }
}
