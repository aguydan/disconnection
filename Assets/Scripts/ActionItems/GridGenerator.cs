using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] RectTransform _leftPage;
    [SerializeField] RectTransform _rightPage;
    [SerializeField] GameObject _testCircle;
    [SerializeField] GameObject _rightCell;
    [SerializeField] int _width, _height;
    [SerializeField] int _gap;
    [SerializeField] int _wordWidth;
    
    Dictionary<Vector2, GameObject> _leftCells = new Dictionary<Vector2, GameObject>();
    Dictionary<int, GameObject> _rightCells = new Dictionary<int, GameObject>();

    public void CreateLeftGrid()
    {
        Vector2 initialPos = new Vector2(0, -_gap);
        Vector2 temporaryUp = initialPos;
        
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                GameObject circle = Instantiate(_testCircle);
                circle.transform.SetParent(_leftPage.transform);
                circle.transform.localPosition = temporaryUp += new Vector2(0, _gap);

                _leftCells.Add(new Vector2(x, y), circle);
            }
            
            initialPos += new Vector2(_gap, 0);
            temporaryUp = initialPos;
        }

        foreach (KeyValuePair<Vector2, GameObject> cell in _leftCells)
        {
            float x = cell.Value.transform.localPosition.x - (_gap * (_width - 1)) / 2;
            float y = cell.Value.transform.localPosition.y - (_gap * (_height - 1)) / 2;
            
            cell.Value.transform.localPosition = new Vector2(x, y);
        }
    }

    public void CreateRightGrid()
    {
        Vector2 initialPos = new Vector2(-_gap, 0);
        
        for (int x = 0; x < _wordWidth; x++)
        {
            GameObject circle = Instantiate(_rightCell);
            circle.transform.SetParent(_rightPage.transform);
            circle.transform.localPosition = initialPos += new Vector2(_gap, 0);

            _rightCells.Add(x, circle);
        }

        foreach (KeyValuePair<int, GameObject> cell in _rightCells)
        {
            float x = cell.Value.transform.localPosition.x - (_gap * (_wordWidth - 1)) / 2;
            
            cell.Value.transform.localPosition = new Vector2(x, 0);
        }
    }

    public void PopulateLeftGrid()
    {
        for (int y = 0; y < _height; y++)
        {
            
        }
    }
}
