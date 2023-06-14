using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform _escapismBack;
    [SerializeField] private Animator _escapismAnimator;
    [SerializeField] private Transform _moodBack;
    [SerializeField] private Animator _moodAnimator;
    [SerializeField] private TextMeshProUGUI _escapismDebugText;
    [SerializeField] private TextMeshProUGUI _moodDebugText;
    [SerializeField] private TextMeshProUGUI _updateScorePrefab;

    public Color EscapismColor;
    public Color MoodColor;

    public static ScoreManager instance;

    private enum ScoreType
    {
        Escapism,
        Mood
    }
    
    private void Awake()
    {
        instance = this;
        UpdateMaxItemAmount();
    }
    
    private void Start()
    {
        UpdateScoreDebugText(ScoreType.Escapism);
        UpdateScoreDebugText(ScoreType.Mood);
    }

    public void IncreaseEscapism()
    {
        Scoring.EscapismScore += 20;
        UpdateScoreDebugText(ScoreType.Escapism);
        _escapismAnimator.Play("EscapismScoreUpdate");
        CreateScoreUpdateNotification("+", ScoreType.Escapism);
        UpdateMaxItemAmount();
    }

    public void DecreaseEscapism()
    {
        Scoring.EscapismScore -= 5;

        if (Scoring.EscapismScore <= 0)
        {
            StartCoroutine(GameManager.instance.GameOver());
        }
        else
        {
            UpdateScoreDebugText(ScoreType.Escapism);
            _escapismAnimator.Play("EscapismScoreUpdate");
            CreateScoreUpdateNotification("-", ScoreType.Escapism);
            UpdateMaxItemAmount();
        }
    }

    public void IncreaseMood(int points)
    {
        Scoring.MoodScore += points;
        UpdateScoreDebugText(ScoreType.Mood);
        _moodAnimator.Play("Wiggle"); //to do
        CreateScoreUpdateNotification("+", ScoreType.Mood);
    }

    public void DecreaseMood(int points)
    {
        Scoring.MoodScore -= points;
        UpdateScoreDebugText(ScoreType.Mood);
        _moodAnimator.Play("Wiggle");
        CreateScoreUpdateNotification("-", ScoreType.Mood);
    }
    
    void UpdateScoreDebugText(ScoreType type)
    {
        if (type == ScoreType.Escapism)
        {
            _escapismDebugText.text = Scoring.EscapismScore.ToString();
        }
        else
        {
            _moodDebugText.text = Scoring.MoodScore.ToString();
        }
    }

    void CreateScoreUpdateNotification(string sign, ScoreType type)
    {
        _updateScorePrefab.text = sign;
        
        if (type == ScoreType.Escapism)
        {
            _updateScorePrefab.color = EscapismColor;
            TextMeshProUGUI text = Instantiate(_updateScorePrefab);
            text.transform.SetParent(_escapismBack, false);
            Destroy(text.gameObject, 2f);
        }
        else
        {
            _updateScorePrefab.color = MoodColor;
            TextMeshProUGUI text = Instantiate(_updateScorePrefab);
            text.transform.SetParent(_moodBack, false);
            Destroy(text.gameObject, 2f);
        }
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
