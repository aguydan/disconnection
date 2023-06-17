using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WinningConstellation
{
    public string ItemName;
    public Sprite Sprite;
}

public class TelescopeDisplay : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _overlay;
    [SerializeField] private float _overlaySpeed = 40f;
    
    [SerializeField] private RectTransform _constellations;
    [SerializeField] private Image _constellPrefab;
    [SerializeField] private int _constellAmount;
    [SerializeField] private Vector2 _spawnBorder1;
    [SerializeField] private Vector2 _spawnBorder2;
    [SerializeField] private Sprite[] _fillerStars;
    [SerializeField] private WinningConstellation[] _winningStars;

    private Vector2 _movement;
    private List<Vector2> _spawnPoints = new List<Vector2>();
    
    public void GenerateConstellations()
    {
        GenerateSpawnPoints();
        
        int winningIndex = Random.Range(0, _spawnPoints.Count);
        
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            Image constell = Instantiate(_constellPrefab, _spawnPoints[i], Quaternion.identity); //
            
            if (i == winningIndex)
            {
                //НА ОСНОВЕ ВИННИНГ СПРАЙТА!!!!
                constell.sprite = _winningStars[0].Sprite;
            }
            else
            {
                constell.sprite = _fillerStars[Random.Range(0, _fillerStars.Length)];
            }
            
            constell.transform.SetParent(_constellations, false);
        }
    }

    private void GenerateSpawnPoints()
    {
        int gridWidth = Mathf.CeilToInt(_constellAmount * 0.64f);
        int gridHeight = Mathf.FloorToInt(_constellAmount * 0.36f);

        float horizontalStep = (_spawnBorder2.x * 2) / gridWidth;
        float verticalStep = (_spawnBorder2.y * 2) / gridHeight;

        horizontalStep += horizontalStep / gridWidth;
        verticalStep += verticalStep / gridHeight;

        Vector2 currentPoint = _spawnBorder1;

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Vector2 offsetPoint = currentPoint + Random.insideUnitCircle * 90;
                _spawnPoints.Add(offsetPoint);
                currentPoint += new Vector2(horizontalStep, 0);
            }

            currentPoint = new Vector2(_spawnBorder1.x, currentPoint.y + verticalStep);
        }
    }
    
    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _overlay.MovePosition(_overlay.position + _movement.normalized * _overlaySpeed * Time.fixedDeltaTime);
    }
}
