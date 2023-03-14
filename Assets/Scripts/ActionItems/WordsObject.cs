using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordGroup
{
    public string Key;
    public string[] Words;
}

[CreateAssetMenu(menuName = "WordsObject")]
public class WordsObject : ScriptableObject
{
    public WordGroup[] WordGroups;
}
