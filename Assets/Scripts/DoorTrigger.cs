using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // КОЛЛАЙДЕР КОЛЛАЙДИТСЯ СО СТЕНАМИ НА СЛОЕ ИГРОК!!! ОСТОРОЖНО!!!
        if (Scoring.EscapismScore >= 100)
        {
            if (Scoring.MoodScore <= 3)
            {
                StartCoroutine(GameManager.instance.ContinueToNextScene("BadEnding"));
            }
            else
            {
                StartCoroutine(GameManager.instance.ContinueToNextScene("Finale"));
            }
        }
        else
        {
            StartCoroutine(GameManager.instance.ContinueToNextScene("Game"));
        }
    }
}
