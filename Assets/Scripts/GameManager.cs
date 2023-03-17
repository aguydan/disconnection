using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Hero hero;
    [SerializeField] GameOverScreen gameOverScreen;
    [SerializeField] GameObject doorPrefab;
    [SerializeField] FurnitureObject _furnitureSets;

    [SerializeField] Button _VRButton;
    [SerializeField] Button _musicPlayerButton;
    [SerializeField] Button _bookButton;

    public static GameManager instance;
    public Vector2 heroPosition;
    public bool IsLevelCompleted { get; private set; } = false;

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
        int index = FurnitureSpawner.Instance.CurrentFurnitureSetIndex;
        
        Instantiate(doorPrefab, _furnitureSets.FurnitureSets[index].DoorSpawnPosition, Quaternion.identity);
        IsLevelCompleted = true;

        _VRButton.interactable = false;
        _musicPlayerButton.interactable = false;
        _bookButton.interactable = false;
    }
}
