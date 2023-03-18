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
        Scoring.EscapismScore = 20;
        Scoring.MaxItemAmount = 50;
        SceneManager.LoadScene("Game");
    }

    public void ExitToMenu()
    {
        Scoring.EscapismScore = 20;
        SceneManager.LoadScene("Menu");
    }
}
