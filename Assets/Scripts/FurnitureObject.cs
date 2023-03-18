using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FurnitureSet
{
    public GameObject Room;
    public Furniture[] Furniture;
    public Vector2 DoorSpawnPosition;
}

[CreateAssetMenu(menuName = "FurnitureObject")]
public class FurnitureObject : ScriptableObject
{
    public FurnitureSet[] FurnitureSets;
}
