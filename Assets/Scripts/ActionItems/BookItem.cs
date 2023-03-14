using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LetterBlueprint
{
    public string LetterName;
    public Letter letterPrefab;
}

public class BookItem : MonoBehaviour
{
    [SerializeField] GridGenerator _gridGenerator;
    [SerializeField] WordsObject _wordsObject;
    [SerializeField] LetterBlueprint[] _alphabetBlueprint;

    public static BookItem Instance;
    public Dictionary<string, Letter> Alphabet = new Dictionary<string, Letter>();

    //может сделать его char чтобы постоянно не тустрингить

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
        for (int i = 0; i < _alphabetBlueprint.Length; i++)
        {
            Alphabet.Add(_alphabetBlueprint[i].LetterName, _alphabetBlueprint[i].letterPrefab);
        }
    }
}
