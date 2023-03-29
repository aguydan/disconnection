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
        float fraction = 360 / Scoring.WinningItemSprites.Count;
        float currentFraction = fraction;
        
        foreach (Sprite sprite in Scoring.WinningItemSprites)
        {
            Vector2 spawnPosition = new Vector2(Mathf.Cos(currentFraction), Mathf.Sin(currentFraction));
            
            Item item = Instantiate(_itemPrefab, spawnPosition * 4, Quaternion.Euler(0, 0, Random.Range(0, 40)));
            item.look.sprite = sprite;

            currentFraction += (fraction + fraction / Scoring.WinningItemSprites.Count);
        }
    }
}
