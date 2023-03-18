using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [SerializeField] private FurnitureObject _furnitureSets;
    [SerializeField] private GameObject _furnitureParent;
    public InteractableFurniture[] InteractableFurniturePrefabs;
    [SerializeField] private GameObject _interactableFurnitureParent;

    public static FurnitureSpawner Instance;

    public int CurrentFurnitureSetIndex;
    Furniture[] _currentFurniture;
    public List<InteractableFurniture> SpawnedInteractables = new List<InteractableFurniture>();
    public List<Furniture> SpawnedFurniture = new List<Furniture>();
    public InteractableFurniture WinningFurniture; // public ли?

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        CurrentFurnitureSetIndex = Random.Range(0, 2);
        CurrentFurnitureSetIndex = 0; //ВРЕММЕННО!!!!

        _currentFurniture = _furnitureSets.FurnitureSets[CurrentFurnitureSetIndex].Furniture;
        SpawnFurniture();
        SpawnInteractableFurniture();
    }

    void SpawnFurniture()
    {
        GameObject room = _furnitureSets.FurnitureSets[CurrentFurnitureSetIndex].Room;
        Instantiate(room, room.transform.position, Quaternion.identity);
        
        foreach (Furniture prefab in _currentFurniture)
        {
            Furniture furniture = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
            furniture.transform.SetParent(_furnitureParent.transform);

            SpawnedFurniture.Add(furniture);
        }
    }

    void SpawnInteractableFurniture()
    {
        int winningFurnitureIndex = Random.Range(0, _currentFurniture.Length);
        
        foreach (Furniture prefab in _currentFurniture)
        {
            int randomIndex = Random.Range(0, InteractableFurniturePrefabs.Length);
            
            InteractableFurniture furniture = Instantiate(InteractableFurniturePrefabs[randomIndex], prefab.transform.position, Quaternion.identity);
            furniture.transform.SetParent(_interactableFurnitureParent.transform);
            furniture.Collider.enabled = false;

            if (winningFurnitureIndex == SpawnedInteractables.Count)
            {
                furniture.HasHint = true;
                WinningFurniture = furniture;
            }

            SpawnedInteractables.Add(furniture);
        }
    }
}
