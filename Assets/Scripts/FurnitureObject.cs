using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FurnitureSet
{
    public GameObject Room;
    public Furniture[] Furniture;
    public GameObject[] FurnitureShadows;
    public Vector2 DoorSpawnPosition;
    public GameObject[] NorthWalls;
    public Vector2 ItemSpawnPoint1;
    public Vector2 ItemSpawnPoint2;
}

[CreateAssetMenu(menuName = "FurnitureObject")]
public class FurnitureObject : ScriptableObject
{
    public FurnitureSet[] FurnitureSets;
}
