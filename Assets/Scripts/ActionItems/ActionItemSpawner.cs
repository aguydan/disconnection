using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItemSpawner : MonoBehaviour
{
    [SerializeField] ActionItem _VRPefab;
    [SerializeField] ActionItem _bookPefab;
    [SerializeField] ActionItem _musicPlayerPefab;
    
    public static ActionItemSpawner Instance;
    public Dictionary<string, int> AmountOfActionItems = new Dictionary<string, int>()
    {
        { "VR", 0 },
        { "musicPlayer", 0 },
        { "book", 0 }
    };
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnActionItems();
    }

    void SpawnActionItems()
    {
        for (int i = 0; i < 2; i++)
        {
            int chance = Random.Range(0, 3);

            if (chance == 1)
            {
                Instantiate(_bookPefab, new Vector2(Random.Range(-7f, 7f), Random.Range(-3.5f, 2f)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
                AmountOfActionItems["book"]++;
            }

            chance = Random.Range(0, 3);

            if (chance == 1)
            {
                Instantiate(_musicPlayerPefab, new Vector2(Random.Range(-7f, 7f), Random.Range(-3.5f, 2f)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
                AmountOfActionItems["musicPlayer"]++;
            }
        }

        if ((AmountOfActionItems["book"] == 0 && AmountOfActionItems["musicPlayer"] == 0) || ((AmountOfActionItems["book"] == 1 || AmountOfActionItems["musicPlayer"] == 1)))
        {
            Instantiate(_VRPefab, new Vector2(Random.Range(-7f, 7f), Random.Range(-3.5f, 2f)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
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
                Instantiate(_VRPefab, new Vector2(Random.Range(-7f, 7f), Random.Range(-3.5f, 2f)), Quaternion.Euler(0, 0, Random.Range(0f, 40f)));
                AmountOfActionItems["VR"]++;
            }
        }
    }
}