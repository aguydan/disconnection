using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LetterBlueprint
{
    public char LetterName;
    public Letter letterPrefab;
}

public class BookItem : MonoBehaviour
{
    //КНИГА НЕ РАБОТАЕТ ПОТОМУ ЧТО НЕ ВКЛЮЧЕНА В РЕДАКТОРЕ!!!
    
    [SerializeField] GridGenerator _gridGenerator;
    [SerializeField] WordsObject _wordsObject;
    [SerializeField] LetterBlueprint[] _alphabetBlueprint;
    [SerializeField] Image _face;
    [SerializeField] Sprite[] _possibleFaces;
    [SerializeField] Image _itemSilhouette;

    public static BookItem Instance;
    public Dictionary<char, Letter> Alphabet = new Dictionary<char, Letter>();
    public bool IsBookCompleted = false;
    string _currentWord;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PopulateAlphabet();
        _gridGenerator.CreateLeftSpawnPoints();
        gameObject.SetActive(false);
    }

    void searchForWord(int currentTry)
    {
        string winningItemName = ItemSpawner.Instance.winningItem.look.sprite.name;
        
        foreach (WordGroup group in _wordsObject.WordGroups)
        {
            if (group.Key == winningItemName)
            {
                _currentWord = group.Words[currentTry];
                break;
            }
        }
    }

    public void CreateAndPopulateGrids(int currentTry)
    {
        _gridGenerator.ClearGrids();
        searchForWord(currentTry);
        _gridGenerator.CreateLeftGrid();
        _gridGenerator.CreateRightGrid(_currentWord.Length);
        _gridGenerator.PopulateCellsWithLetters(_currentWord);
        _face.sprite = _possibleFaces[0];


        _itemSilhouette.sprite = ItemSpawner.Instance.winningItem.look.sprite;
        _itemSilhouette.SetNativeSize();
    }

    void PopulateAlphabet()
    {
        foreach (LetterBlueprint blueprint in _alphabetBlueprint)
        {
            Alphabet.Add(blueprint.LetterName, blueprint.letterPrefab);
        }
    }

    public void UpdateFace()
    {
        string computedWord = ComputeCurrentWord();
        float percentage = CompareWords(_currentWord, computedWord);

        Debug.Log(percentage);

        if (percentage <= 25)
        {
            _face.sprite = _possibleFaces[0];
        }
        else if (percentage > 25 && percentage <= 50)
        {
            _face.sprite = _possibleFaces[1];
        }
        else if (percentage > 61 && percentage < 100)
        {
            _face.sprite = _possibleFaces[2];
        }
        else if (percentage == 100)
        {
            _face.sprite = _possibleFaces[3];
            IsBookCompleted = true;
            StartCoroutine(ActionItemManager.instance.DeactivateBookAutomatically());
        }
    }

    string ComputeCurrentWord()
    {
        List<char> currentLetters = new List<char>();
        StringBuilder sb = new StringBuilder();

        foreach (KeyValuePair<int, Cell> cell in _gridGenerator._rightCells)
        {
            if (cell.Value.IsOccupied)
            {
                GameObject child = cell.Value.transform.GetChild(0).gameObject;
                Letter letter = child.GetComponent<Letter>();
                currentLetters.Add(letter.Name);
            }
            else
            {
                currentLetters.Add(' ');
            }
        }

        foreach (char letter in currentLetters)
        {
            sb.Append(letter);
        }

        return sb.ToString();
    }
    
    float CompareWords(string word, string computedWord)
    {
        int computedWordLength = computedWord.Length;
        float score = 0;
        float wordLength = word.Length;

        for (int i = 0; i < computedWordLength; i++)
        {
            if (word[i] != computedWord[i]) break;
            score++;
        }

        return (score / wordLength) * 100;
    }
}
