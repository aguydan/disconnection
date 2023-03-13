using System.Collections.Generic;
using UnityEngine;

//У КАМЕРЫ ВЫКЛЮЧЕН ПОСТ ПРОСЕССИНГ!!!!!

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Item itemPrefab;
    [SerializeField] Material standart;
    [SerializeField] Material outline;
    [SerializeField] RandomSpriteGenerator spriteGenerator;

    List<Item> spawnedItems = new List<Item>();
    int winningItemIndex;
    [SerializeField] Item winningItem;
    int maxDistance = 5;

    void Start()
    {
        winningItemIndex = Random.Range(0, 50);

        for (int i = 0; i < 50; i++)
        {
            SpawnRandomItem(new Vector2(Random.Range(-7f, 7f), Random.Range(-3.5f, 2f)));
        }

        winningItem.look.color = Color.green;
    }

    void SpawnRandomItem(Vector2 position)
    {
        ItemSprite sprite = spriteGenerator.GetRandomSprite();
        itemPrefab.look.sprite = sprite.Sprite;
        itemPrefab.capsuleCollider.size = new Vector2(sprite.Sprite.bounds.size.x, sprite.Sprite.bounds.size.y);

        Item item = Instantiate(itemPrefab, position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));

        if (winningItemIndex == spawnedItems.Count)
        {
            item.hasPositivePoints = true;
            winningItem = item;
        }

        spawnedItems.Add(item);
    }

    public void CompleteChallenge()
    {
        List<Item> currentItems = new List<Item>();

        foreach(Item item in spawnedItems) {
            if (item) currentItems.Add(item);
        }

        UpdateHints(winningItem, currentItems, maxDistance);

        maxDistance -= 2;
    }

    void UpdateHints(Item winningItem, List<Item> currentItems, int maxDistance)
    {
        RestoreStandartMaterials(currentItems);
        for (int i = 0; i < currentItems.Count; i++)
        {
            float distance = Vector2.Distance(winningItem.transform.position, currentItems[i].transform.position);

            if (distance <= maxDistance)
            {
                currentItems[i].look.material = outline;
            }
        }
    }
    
    void RestoreStandartMaterials(List<Item> currentItems) {
        foreach (Item item in currentItems) {
            item.look.material = standart;
        }
    }
}
