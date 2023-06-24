using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Animator _escapismAnimator;
    [SerializeField] private Image _escapismImage;
    [SerializeField] private Animator _moodAnimator;
    [SerializeField] private Image _moodImage;
    [SerializeField] private TextMeshProUGUI _escapismDebugText;
    [SerializeField] private TextMeshProUGUI _moodDebugText;
    [SerializeField] private TextMeshProUGUI _updateScorePrefab;

    [SerializeField] private Transform _scorePanel;
    [SerializeField] private Sprite[] _escapismSprites;
    [SerializeField] private Sprite[] _moodSprites;

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
        DetermineSpritesForScores();
    }

    public void IncreaseEscapism(int points)
    {
        Scoring.EscapismScore += points;
        UpdateScoreDebugText(ScoreType.Escapism);
        DetermineSpritesForScores();
        _escapismAnimator.Play("Score Update");
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
            DetermineSpritesForScores();
            _escapismAnimator.Play("Score Update");
            CreateScoreUpdateNotification("-", ScoreType.Escapism);
            UpdateMaxItemAmount();
        }
    }

    public void IncreaseMood(int points)
    {
        Scoring.MoodScore += points;
        UpdateScoreDebugText(ScoreType.Mood);
        DetermineSpritesForScores();
        _moodAnimator.Play("Score Update"); //to do
        CreateScoreUpdateNotification("+", ScoreType.Mood);
    }

    public void DecreaseMood(int points, bool isZero = false)
    {
        if (isZero)
        {
            Scoring.MoodScore = 0;
        }
        else
        {
            Scoring.MoodScore -= points;
        }
        
        UpdateScoreDebugText(ScoreType.Mood);
        DetermineSpritesForScores();
        _moodAnimator.Play("Score Update");
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
            TextMeshProUGUI text = Instantiate(_updateScorePrefab, _updateScorePrefab.transform.position - new Vector3(155, 0, 0), Quaternion.identity);
            text.transform.SetParent(_scorePanel, false);
            Destroy(text.gameObject, 2f);
        }
        else
        {
            _updateScorePrefab.color = MoodColor;
            TextMeshProUGUI text = Instantiate(_updateScorePrefab);
            text.transform.SetParent(_scorePanel, false);
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

    private void DetermineSpritesForScores()
    {
        if (Scoring.EscapismScore <= 20)
        {
            _escapismImage.sprite = _escapismSprites[0];
        }
        else if (Scoring.EscapismScore > 20 && Scoring.EscapismScore <= 40)
        {
            _escapismImage.sprite = _escapismSprites[1];
        }
        else if (Scoring.EscapismScore > 40 && Scoring.EscapismScore <= 60)
        {
            _escapismImage.sprite = _escapismSprites[2];
        }
        else if (Scoring.EscapismScore > 60 && Scoring.EscapismScore <= 80)
        {
            _escapismImage.sprite = _escapismSprites[3];
        }
        else if (Scoring.EscapismScore >= 90)
        {
            _escapismImage.sprite = _escapismSprites[4];
        }

        if (Scoring.MoodScore <= 3)
        {
            _moodImage.sprite = _moodSprites[0];
        }
        else if (Scoring.MoodScore > 3 && Scoring.MoodScore <= 6)
        {
            _moodImage.sprite = _moodSprites[1];
        }
        else if (Scoring.MoodScore > 6 && Scoring.MoodScore <= 9)
        {
            _moodImage.sprite = _moodSprites[2];
        }
        else if (Scoring.MoodScore > 9 && Scoring.MoodScore <= 12)
        {
            _moodImage.sprite = _moodSprites[3];
        }
        else if (Scoring.MoodScore > 12)
        {
            _moodImage.sprite = _moodSprites[4];
        }
    }
}
