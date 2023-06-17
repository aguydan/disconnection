using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItemSpawner : MonoBehaviour
{
    [SerializeField] ActionItem _VRPefab;
    [SerializeField] ActionItem _bookPefab;
    [SerializeField] ActionItem _musicPlayerPefab;
    [SerializeField] ActionItem _SMPrefab;
    [SerializeField] FurnitureObject _furnitureSets;
    
    public static ActionItemSpawner Instance;
    public Dictionary<string, int> AmountOfActionItems = new Dictionary<string, int>()
    {
        { "VR", 0 },
        { "musicPlayer", 0 },
        { "book", 0 },
        { "SM", 0 }
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
        //spawn music players or/and books
        SpawnBooksAndMusicPlayers();

        //decide if we should spawn VR as well
        SpawnVRs();
        
        //spawn social media NEEDS CHANGING IF A NEW ROOM IS BEING ADDED!!!!
        SpawnSocialMedia();

        void SpawnBooksAndMusicPlayers()
        {
            for (int i = 0; i < 2; i++)
            {
                int chance = Random.Range(0, 3);

                if (chance == 1 && !(Scoring.FurnSetIndex == Scoring.TelescopeRoomIndex))
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
        }

        void SpawnVRs()
        {
            if (Scoring.FurnSetIndex == Scoring.TelescopeRoomIndex) return;
            
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

        void SpawnSocialMedia()
        {
            if (!Scoring.AreSMLocationsChosen)
            {
                Scoring.SMLocation1 = Random.Range(0, 5);
                Scoring.SMLocation2 = Scoring.SMLocation1 + Random.Range(1, 4);
                
                Scoring.AreSMLocationsChosen = true;
            }
            
            if (Scoring.MaxSocialMediaItemsAmount == 0) return;

            if (Scoring.SMLocation2 > 4)
            {
                Scoring.SMLocation2 -= 5;
            }

            if (Scoring.FurnSetIndex == Scoring.SMLocation1 || Scoring.FurnSetIndex == Scoring.SMLocation2)
            {
                Instantiate(_SMPrefab, new Vector2(Random.Range(_spawnPoint1.x, _spawnPoint2.x), Random.Range(_spawnPoint1.y, _spawnPoint2.y)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
                Scoring.MaxSocialMediaItemsAmount--;
            }

            Debug.Log(Scoring.SMLocation1 + " " + Scoring.SMLocation2);
        }
    }
}
