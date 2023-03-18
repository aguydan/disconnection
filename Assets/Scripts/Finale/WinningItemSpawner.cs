using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningItemSpawner : MonoBehaviour
{
    [SerializeField] Item _itemPrefab;
    // [SerializeField] Sprite[] _testingSprites;
    
    void Start()
    {
        SpawnWinnnigItems();
    }

    void SpawnWinnnigItems()
    {
        foreach (Sprite sprite in Scoring.WinningItemSprites)
        {
            Vector2 positionAroundCenter = Random.insideUnitCircle.normalized * 2.5f;
            int tries = 0;
            
            while (Physics2D.OverlapCircle(positionAroundCenter, 1) && tries < 20)
            {
                positionAroundCenter = Random.insideUnitCircle.normalized * 2.5f;
                tries++;
            }
            
            Item item = Instantiate(_itemPrefab, positionAroundCenter, Quaternion.Euler(0, 0, Random.Range(0, 40)));
            item.look.sprite = sprite;
        }
    }
}
