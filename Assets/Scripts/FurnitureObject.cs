using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FurnitureSet
{
    public GameObject[] Furniture;
}

[CreateAssetMenu(menuName = "FurnitureObject")]
public class FurnitureObject : ScriptableObject
{
    public FurnitureSet[] FurnitureSets;
}
