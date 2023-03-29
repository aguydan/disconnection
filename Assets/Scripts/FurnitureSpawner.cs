using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [SerializeField] private FurnitureObject _furnitureSets;
    [SerializeField] private GameObject _furnitureParent;
    public InteractableFurniture[] InteractableFurniturePrefabs;
    [SerializeField] private GameObject _interactableFurnitureParent;
    [SerializeField] private GameObject _wallsLayer;

    public static FurnitureSpawner Instance;

    public int CurrentFurnitureSetIndex;
    Furniture[] _currentFurniture;
    public List<InteractableFurniture> SpawnedInteractables = new List<InteractableFurniture>();
    public List<Furniture> SpawnedFurniture = new List<Furniture>();
    public InteractableFurniture WinningFurniture; // public ли?

    void Awake()
    {
        Instance = this;
        
        Scoring.FurnSetIndex++;

        if (Scoring.FurnSetIndex == 5)
        {
            
            Scoring.FurnSetIndex = 0;
        }

        CurrentFurnitureSetIndex = Scoring.FurnSetIndex;
    }
    
    void Start()
    {
        _currentFurniture = _furnitureSets.FurnitureSets[CurrentFurnitureSetIndex].Furniture;
        SpawnNorthWalls();
        SpawnFurniture();
        SpawnInteractableFurniture();
    }

    void SpawnNorthWalls()
    {
        GameObject[] walls = _furnitureSets.FurnitureSets[CurrentFurnitureSetIndex].NorthWalls;

        foreach (GameObject wallPrefab in walls)
        {
            GameObject wall = Instantiate(wallPrefab, wallPrefab.transform.position, Quaternion.identity);
            wall.transform.SetParent(_wallsLayer.transform);
        }
    }

    void SpawnFurniture()
    {
        GameObject room = _furnitureSets.FurnitureSets[CurrentFurnitureSetIndex].Room;
        Instantiate(room, room.transform.position, Quaternion.identity);
        GameObject[] shadows = _furnitureSets.FurnitureSets[CurrentFurnitureSetIndex].FurnitureShadows;

        foreach (GameObject shadow in shadows)
        {
            Instantiate(shadow, shadow.transform.position, Quaternion.identity);
        }
        
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
