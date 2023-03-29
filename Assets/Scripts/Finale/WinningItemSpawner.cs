using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningItemSpawner : MonoBehaviour
{
    [SerializeField] Item _itemPrefab;
    
    void Start()
    {
        SpawnWinnnigItems();
    }

    void SpawnWinnnigItems()
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
}
