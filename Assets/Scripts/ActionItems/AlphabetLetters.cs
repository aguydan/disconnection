using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Letter
{
    public string Name;
    public GameObject letterPrefab;
}

[CreateAssetMenu(menuName = "AlphabetLetters")]
public class AlphabetLetters : ScriptableObject
{
    public Letter[] Letters;
}
