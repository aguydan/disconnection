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
        GameManager.instance.ResetStartingStats();
        SoundManager.Instance.StopMusic();
        StartCoroutine(GameManager.instance.ContinueToNextScene("Game"));
       
    }

    public void ExitToMenu()
    {
        GameManager.instance.ResetStartingStats();
        SoundManager.Instance.StopMusic();
        StartCoroutine(GameManager.instance.ContinueToNextScene("Menu"));
    }
}
