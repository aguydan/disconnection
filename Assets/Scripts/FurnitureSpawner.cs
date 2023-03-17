using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    private int _currentFurnitureSet;

    public static FurnitureSpawner Instance;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _currentFurnitureSet = Random.Range(0, 5);
    }

    //spawn furniture
    //spawn interactable furniture
    //pick winning furniture
}
