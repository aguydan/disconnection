using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] RectTransform _leftPage;
    [SerializeField] RectTransform _rightPage;
    [SerializeField] GameObject _spawnPoint;
    [SerializeField] Cell _leftCell;
    [SerializeField] Cell _rightCell;
    [SerializeField] int _width, _height;
    [SerializeField] int _gap;
    [SerializeField] char[] _alphabet;
    
    Dictionary<Vector2, GameObject> _spawnPoints = new Dictionary<Vector2, GameObject>();
    public Dictionary<int, Cell> _rightCells = new Dictionary<int, Cell>();
    List<Cell> _spawnedLeftCells = new List<Cell>();
    List<Letter> _spawnedLetters = new List<Letter>();

    public void CreateLeftSpawnPoints()
    {
        Vector2 initialPos = new Vector2(0, -_gap);
        Vector2 temporaryUp = initialPos;
        
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                GameObject spawnPoint = Instantiate(_spawnPoint);
                spawnPoint.transform.SetParent(_leftPage.transform);
                
                spawnPoint.transform.localPosition = temporaryUp += new Vector2(0, _gap);
                float finalX = spawnPoint.transform.localPosition.x - (_gap * (_width - 1)) / 2;
                float finalY = spawnPoint.transform.localPosition.y - (_gap * (_height - 1)) / 2;
                spawnPoint.transform.localPosition = new Vector2(finalX, finalY);

                _spawnPoints.Add(new Vector2(x, y), spawnPoint);
            }
            
            initialPos += new Vector2(_gap, 0);
            temporaryUp = initialPos;
        }
    }

    public void CreateRightGrid(int wordWidth)
    {
        Vector2 initialPos = new Vector2(-_gap, 0);
        
        for (int x = 0; x < wordWidth; x++)
        {
            Cell cell = Instantiate(_rightCell);
            cell.transform.SetParent(_rightPage.transform);

            cell.transform.localPosition = initialPos += new Vector2(_gap, 0);
            float finalX = cell.transform.localPosition.x - (_gap * (wordWidth - 1)) / 2;
            cell.transform.localPosition = new Vector2(finalX, 0);
            cell.IsRightCell = true;

            _rightCells.Add(x, cell);
        }
    }

    public void CreateLeftGrid()
    {
        int wordBegin;
        int wordEnd;
        
        for (int y = 0; y < _height; y++)
        {
            wordBegin = Random.Range(0, _width);
            wordEnd = Random.Range(wordBegin, _width);

            if (wordEnd - wordBegin < 3)
            {
                wordEnd += 2;
                wordEnd = Mathf.Min(wordEnd, _width);
            }

            for (int i = wordBegin; i < wordEnd; i++)
            {
                Cell cell = Instantiate(_leftCell);
                cell.transform.SetParent(_leftPage.transform);
                cell.transform.position = _spawnPoints[new Vector2(i, y)].transform.position;

                _spawnedLeftCells.Add(cell);
            }
        }
    }

    public void PopulateCellsWithLetters(string word)
    {
        Letter letter = Instantiate(BookItem.Instance.Alphabet[word[0]]);
        letter.transform.SetParent(_rightCells[0].transform);
        letter.transform.position = _rightCells[0].transform.position;

        _rightCells[0].IsOccupied = true;
        _spawnedLetters.Add(letter);
        
        for (int i = 1; i < word.Length; i++)
        {
            int leftCellsCount = _spawnedLeftCells.Count;
            int randomCellIndex = Random.Range(0, leftCellsCount);

            while (_spawnedLeftCells[randomCellIndex].IsOccupied)
            {
                randomCellIndex = Random.Range(0, leftCellsCount);
            }
            
            Letter letter1 = Instantiate(BookItem.Instance.Alphabet[word[i]]);
            letter1.transform.SetParent(_spawnedLeftCells[randomCellIndex].transform);
            letter1.transform.position = _spawnedLeftCells[randomCellIndex].transform.position;

            _spawnedLeftCells[randomCellIndex].IsOccupied = true;
            _spawnedLetters.Add(letter1);
        }

        foreach (Cell cell in _spawnedLeftCells)
        {
            if (cell.IsOccupied) continue;

            int randomLetterIndex = Random.Range(0, 33);
            
            Letter letter1 = Instantiate(BookItem.Instance.Alphabet[_alphabet[randomLetterIndex]]);
            letter1.transform.SetParent(cell.transform);
            letter1.transform.position = cell.transform.position;

            cell.IsOccupied = true;
            _spawnedLetters.Add(letter1);
        }
    }

    public void ClearGrids()
    {
        foreach (KeyValuePair<int, Cell> cell in _rightCells)
        {
            Destroy(cell.Value.gameObject);
        }
        _rightCells.Clear();

        foreach (Cell cell in _spawnedLeftCells)
        {
            Destroy(cell.gameObject);
        }
        _spawnedLeftCells.Clear();

        foreach (Letter letter in _spawnedLetters)
        {
            Destroy(letter.gameObject);
        }
        _spawnedLetters.Clear();
    }
}
