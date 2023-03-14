using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] RectTransform _leftPage;
    [SerializeField] RectTransform _rightPage;
    [SerializeField] GameObject _spawnPoint;
    [SerializeField] Cell _leftCell;
    [SerializeField] GameObject _rightCell;
    [SerializeField] int _width, _height;
    [SerializeField] int _gap;
    
    Dictionary<Vector2, GameObject> _spawnPoints = new Dictionary<Vector2, GameObject>();
    Dictionary<int, GameObject> _rightCells = new Dictionary<int, GameObject>();
    List<Cell> _spawnedLeftCells = new List<Cell>();

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

                _spawnPoints.Add(new Vector2(x, y), spawnPoint);
            }
            
            initialPos += new Vector2(_gap, 0);
            temporaryUp = initialPos;
        }

        foreach (KeyValuePair<Vector2, GameObject> spawnPoint in _spawnPoints)
        {
            float x = spawnPoint.Value.transform.localPosition.x - (_gap * (_width - 1)) / 2;
            float y = spawnPoint.Value.transform.localPosition.y - (_gap * (_height - 1)) / 2;
            
            spawnPoint.Value.transform.localPosition = new Vector2(x, y);
        }

        //эти  лупа можно обьединить
    }

    public void CreateRightGrid(int wordWidth)
    {
        Vector2 initialPos = new Vector2(-_gap, 0);
        
        for (int x = 0; x < wordWidth; x++)
        {
            GameObject cell = Instantiate(_rightCell);
            cell.transform.SetParent(_rightPage.transform);
            cell.transform.localPosition = initialPos += new Vector2(_gap, 0);

            _rightCells.Add(x, cell);
        }

        foreach (KeyValuePair<int, GameObject> cell in _rightCells)
        {
            float x = cell.Value.transform.localPosition.x - (_gap * (wordWidth - 1)) / 2;
            
            cell.Value.transform.localPosition = new Vector2(x, 0);
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
        Letter letter = Instantiate(BookItem.Instance.Alphabet[word[0].ToString()]);
        letter.transform.SetParent(_rightCells[0].transform);
        letter.transform.position = _rightCells[0].transform.position;
        
        // foreach (Cell cell in _spawnedLeftCells)
        // {
            
        // }
    }
}
