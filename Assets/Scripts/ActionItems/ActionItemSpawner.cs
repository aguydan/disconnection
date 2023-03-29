using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItemSpawner : MonoBehaviour
{
    [SerializeField] ActionItem _VRPefab;
    [SerializeField] ActionItem _bookPefab;
    [SerializeField] ActionItem _musicPlayerPefab;
    [SerializeField] FurnitureObject _furnitureSets;
    
    public static ActionItemSpawner Instance;
    public Dictionary<string, int> AmountOfActionItems = new Dictionary<string, int>()
    {
        { "VR", 0 },
        { "musicPlayer", 0 },
        { "book", 0 }
    };
    Vector2 _spawnPoint1;
    Vector2 _spawnPoint2;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _spawnPoint1 = _furnitureSets.FurnitureSets[FurnitureSpawner.Instance.CurrentFurnitureSetIndex].ItemSpawnPoint1;
        _spawnPoint2 = _furnitureSets.FurnitureSets[FurnitureSpawner.Instance.CurrentFurnitureSetIndex].ItemSpawnPoint2;
        SpawnActionItems();
    }

    void SpawnActionItems()
    {
        for (int i = 0; i < 2; i++)
        {
            int chance = Random.Range(0, 3);

            if (chance == 1)
            {
                Instantiate(_bookPefab, new Vector2(Random.Range(_spawnPoint1.x, _spawnPoint2.x), Random.Range(_spawnPoint1.y, _spawnPoint2.y)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
                AmountOfActionItems["book"]++;
            }

            chance = Random.Range(0, 3);

            if (chance == 1)
            {
                Instantiate(_musicPlayerPefab, new Vector2(Random.Range(_spawnPoint1.x, _spawnPoint2.x), Random.Range(_spawnPoint1.y, _spawnPoint2.y)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
                AmountOfActionItems["musicPlayer"]++;
            }
        }

        if ((AmountOfActionItems["book"] == 0 && AmountOfActionItems["musicPlayer"] == 0) || ((AmountOfActionItems["book"] == 1 || AmountOfActionItems["musicPlayer"] == 1)))
        {
            Instantiate(_VRPefab, new Vector2(Random.Range(_spawnPoint1.x, _spawnPoint2.x), Random.Range(_spawnPoint1.y, _spawnPoint2.y)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
            AmountOfActionItems["VR"]++;
        }
        else if (AmountOfActionItems["book"] == 2 && AmountOfActionItems["musicPlayer"] == 2)
        {
            return;
        }
        else
        {
            int chance = Random.Range(0, 3);

            if (chance == 1)
            {
                Instantiate(_VRPefab, new Vector2(Random.Range(_spawnPoint1.x, _spawnPoint2.x), Random.Range(_spawnPoint1.y, _spawnPoint2.y)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
                AmountOfActionItems["VR"]++;
            }
        }
    }
}
