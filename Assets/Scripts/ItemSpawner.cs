using System.Collections.Generic;
using UnityEngine;

//У КАМЕРЫ ВЫКЛЮЧЕН ПОСТ ПРОСЕССИНГ!!!!!

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Item itemPrefab;
    [SerializeField] Material standart;
    [SerializeField] Material outline;
    [SerializeField] RandomSpriteGenerator spriteGenerator;
    [SerializeField] FurnitureObject _furnitureSets;

    [ColorUsage(true, true)]
    [SerializeField] Color[] hintColors;


    public static ItemSpawner Instance;
    List<Item> spawnedItems = new List<Item>();
    public List<Item> ColoredItems = new List<Item>();
    int winningItemIndex;
    public Item winningItem;
    float maxDistance = 5;
    int canUpdateHintsTimes = 2;
    Vector2 _spawnPoint1;
    Vector2 _spawnPoint2;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _spawnPoint1 = _furnitureSets.FurnitureSets[FurnitureSpawner.Instance.CurrentFurnitureSetIndex].ItemSpawnPoint1;
        _spawnPoint2 = _furnitureSets.FurnitureSets[FurnitureSpawner.Instance.CurrentFurnitureSetIndex].ItemSpawnPoint2;
        winningItemIndex = Random.Range(0, Scoring.MaxItemAmount);

        for (int i = 0; i < Scoring.MaxItemAmount; i++)
        {
            SpawnRandomItem(new Vector2(Random.Range(_spawnPoint1.x, _spawnPoint2.x), Random.Range(_spawnPoint1.y, _spawnPoint2.y)));
        }

        ColoredItems = spawnedItems;
    }

    void SpawnRandomItem(Vector2 position)
    {
        int tries = 0;
            
        while (Physics2D.OverlapCircle(position, 1) && tries < 20)
        {
            position = new Vector2(Random.Range(_spawnPoint1.x, _spawnPoint2.x), Random.Range(_spawnPoint1.y, _spawnPoint2.y));
            tries++;
        }
        
        ItemSprite sprite = spriteGenerator.GetRandomSprite();
        itemPrefab.look.sprite = sprite.Sprite;

        Item item = Instantiate(itemPrefab, position, Quaternion.Euler(0, 0, Random.Range(0, 100)));
        item.gameObject.AddComponent<PolygonCollider2D>();

        if (winningItemIndex == spawnedItems.Count)
        {
            item.hasPositivePoints = true;
            winningItem = item;
        }

        spawnedItems.Add(item);
    }

    public void CompleteChallenge()
    {
        outline.color = hintColors[canUpdateHintsTimes];
        List<Item> currentItems = new List<Item>();

        foreach(Item item in spawnedItems)
        {
            if (item) currentItems.Add(item);
        }

        if (canUpdateHintsTimes == 0)
        {
            RestoreStandartMaterials(currentItems);
            ColoredItems.Clear();

            winningItem.look.material = outline;
            ColoredItems.Add(winningItem);
        }
        else
        {
            UpdateHints(winningItem, currentItems, maxDistance);

            maxDistance -= 2.5f;
            canUpdateHintsTimes -= 1;
        }
    }

    void UpdateHints(Item winningItem, List<Item> currentItems, float maxDistance)
    {
        RestoreStandartMaterials(currentItems);
        ColoredItems.Clear();
        for (int i = 0; i < currentItems.Count; i++)
        {
            float distance = Vector2.Distance(winningItem.transform.position, currentItems[i].transform.position);

            if (distance <= maxDistance)
            {
                currentItems[i].look.material = outline;
                ColoredItems.Add(currentItems[i]);
            }
        }
    }
    
    void RestoreStandartMaterials(List<Item> currentItems) {
        foreach (Item item in currentItems) {
            item.look.material = standart;
        }
    }
}
