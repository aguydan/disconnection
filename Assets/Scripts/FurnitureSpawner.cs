using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [SerializeField] private FurnitureObject _furnitureSets;
    [SerializeField] private GameObject _furnitureParent;
    [SerializeField] InteractableFurniture[] _interactableFurniturePrefabs;
    [SerializeField] private GameObject _interactableFurnitureParent;

    public static FurnitureSpawner Instance;

    public int CurrentFurnitureSetIndex;
    GameObject[] _currentFurniture;
    List<InteractableFurniture> _spawnedInteractables = new List<InteractableFurniture>();
    public InteractableFurniture WinningFurniture;

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
        
        foreach (GameObject prefab in _currentFurniture)
        {
            GameObject furniture = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
            furniture.transform.SetParent(_furnitureParent.transform);
        }
    }

    void SpawnInteractableFurniture()
    {
        int winningFurnitureIndex = Random.Range(0, _currentFurniture.Length);
        
        foreach (GameObject prefab in _currentFurniture)
        {
            int randomIndex = Random.Range(0, _interactableFurniturePrefabs.Length);
            
            InteractableFurniture furniture = Instantiate(_interactableFurniturePrefabs[randomIndex], prefab.transform.position, Quaternion.identity);
            furniture.transform.SetParent(_interactableFurnitureParent.transform);
            furniture.BoxCollider.enabled = false;

            if (winningFurnitureIndex == _spawnedInteractables.Count)
            {
                furniture.HasHint = true;
                WinningFurniture = furniture;
            }

            _spawnedInteractables.Add(furniture);
        }
    }
}
