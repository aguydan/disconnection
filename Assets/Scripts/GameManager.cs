using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Hero hero;
    [SerializeField] GameOverScreen gameOverScreen;
    [SerializeField] Door doorPrefab;
    [SerializeField] FurnitureObject _furnitureSets;

    [SerializeField] Button _VRButton;
    [SerializeField] Button _musicPlayerButton;
    [SerializeField] Button _bookButton;

    [SerializeField] GameObject _gameOverBlack;

    public static GameManager instance;
    public Vector2 heroPosition;
    public bool IsLevelCompleted { get; private set; } = false;
    private Door _door;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        int index = FurnitureSpawner.Instance.CurrentFurnitureSetIndex;
        
        _door = Instantiate(doorPrefab, _furnitureSets.FurnitureSets[index].DoorSpawnPosition, Quaternion.identity);
    }

    private void Update()
    {
        heroPosition = hero.heroPosition;
    }

    public IEnumerator GameOver()
    {
        SoundManager.Instance.StopEffects();
        _gameOverBlack.SetActive(true);

        yield return new WaitForSeconds(1);
        
        SoundManager.Instance.PlayMusic(SoundManager.Instance.Music[2]);
        gameOverScreen.EnableScreen();
        _gameOverBlack.SetActive(false);
    }

    public void SpawnDoorToNextLevel()
    {
        _door.DoorProper.SetActive(true);
        _door.Wall.SetActive(false);

        IsLevelCompleted = true;
        SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[8]);

        _VRButton.interactable = false;
        _musicPlayerButton.interactable = false;
        _bookButton.interactable = false;
    }

    public IEnumerator ContinueToNextScene(string sceneName)
    {
        Transition.Instance.CloseTransition();
        
        yield return new WaitForSeconds(1);
        
        SceneManager.LoadScene(sceneName);
    }

    public void ResetStartingStats()
    {
        Scoring.FurnSetIndex = -1;
        Scoring.EscapismScore = 20;
        Scoring.MoodScore = 25;
        Scoring.MaxItemAmount = 50;

        Scoring.WinningItemSprites = new List<Sprite>();
        Scoring.NotepadCategories = new Dictionary<ItemSprite.ItemCategory, Scoring.CategoryInfo>();
        NotepadInitializer.Instance.InitializeNotepadCategories();
    }
}
