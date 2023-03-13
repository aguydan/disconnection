using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Hero hero;
    [SerializeField] GameOverScreen gameOverScreen;
    [SerializeField] GameObject doorPrefab;

    public static GameManager instance;
    public Vector2 heroPosition;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        heroPosition = hero.heroPosition;
    }

    public void GameOver()
    {
        gameOverScreen.EnableScreen();
    }

    public void SpawnDoorToNextLevel()
    {
        Instantiate(doorPrefab, Scoring.doorCoordinates, Quaternion.identity);
    }
}
