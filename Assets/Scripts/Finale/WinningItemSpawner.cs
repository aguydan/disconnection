using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningItemSpawner : MonoBehaviour
{
    [SerializeField] Item _itemPrefab;
    [SerializeField] ParticleSystem _firePrefab;

    //DEBUG
    [SerializeField] Sprite[] _debugSprites;

    public static WinningItemSpawner Instance;
    
    private List<Item> _itemsBad;
    
    private void Awake() => Instance = this;
    
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "BadEnding")
        {
            SpawnWinningItemsBad();
        }
        else
        {
            SpawnWinnnigItemsGood();
        }
    }

    private void SpawnWinnnigItemsGood()
    {
        float itemCount = Scoring.WinningItemSprites.Count;

        for (int i = 0; i < itemCount; i++)
        {
            float angle = i * Mathf.PI * 2 / itemCount;
            Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            Item item = Instantiate(_itemPrefab, spawnPosition * 4, Quaternion.Euler(0, 0, Random.Range(0, 40)));
            item.look.sprite = Scoring.WinningItemSprites[i];
        }
    }

    private void SpawnWinningItemsBad()
    {
        _itemsBad = new List<Item>();
        
        float itemCount = Scoring.WinningItemSprites.Count;

        for (int i = 0; i < _debugSprites.Length; i++) //itemCount!!!
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-4f, 2.2f), Random.Range(-2.2f, -6.4f));

            Item item = Instantiate(_itemPrefab, spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 40)));
            item.look.sprite = _debugSprites[i];
            // item.look.sprite = Scoring.WinningItemSprites[i];

            _itemsBad.Add(item);
        }
    }

    public void SpawnFiresOnItems()
    {
        foreach (Item item in _itemsBad)
        {
            Instantiate(_firePrefab, item.transform.position, _firePrefab.transform.rotation);
        }
    }
}
