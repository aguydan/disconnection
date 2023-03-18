using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // КОЛЛАЙДЕР КОЛЛАЙДИТСЯ СО СТЕНАМИ НА СЛОЕ ИГРОК!!! ОСТОРОЖНО!!!
        if (Scoring.EscapismScore >= 100)
        {
            SceneManager.LoadScene("Finale");
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
}
