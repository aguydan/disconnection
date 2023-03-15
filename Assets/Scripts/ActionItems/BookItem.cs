using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LetterBlueprint
{
    public char LetterName;
    public Letter letterPrefab;
}

public class BookItem : MonoBehaviour
{
    [SerializeField] GridGenerator _gridGenerator;
    [SerializeField] WordsObject _wordsObject;
    [SerializeField] LetterBlueprint[] _alphabetBlueprint;

    public static BookItem Instance;
    public Dictionary<char, Letter> Alphabet = new Dictionary<char, Letter>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PopulateAlphabet();
       _gridGenerator.CreateRightGrid("диск".Length);
       _gridGenerator.CreateLeftSpawnPoints();
       _gridGenerator.CreateLeftGrid();
       _gridGenerator.PopulateCellsWithLetters("диск");
    }

    void PopulateAlphabet()
    {
        foreach (LetterBlueprint blueprint in _alphabetBlueprint)
        {
            Alphabet.Add(blueprint.LetterName, blueprint.letterPrefab);
        }
    }
}
