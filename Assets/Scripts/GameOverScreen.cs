using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void EnableScreen()
    {
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Scoring.FurnSetIndex = -1;
        Scoring.EscapismScore = 20;
        Scoring.MaxItemAmount = 50;
        StartCoroutine(GameManager.instance.ContinueToNextScene("Game"));
       
    }

    public void ExitToMenu()
    {
        Scoring.FurnSetIndex = -1;
        Scoring.EscapismScore = 20;
        StartCoroutine(GameManager.instance.ContinueToNextScene("Menu"));
    }
}
