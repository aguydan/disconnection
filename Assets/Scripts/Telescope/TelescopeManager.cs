using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TelescopeManager : MonoBehaviour
{
    [SerializeField] private Telescope _telescopePrefab;
    [SerializeField] private TelescopeDisplay _telescopeDisplay;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Hero _hero;
    
    public static TelescopeManager Instance;

    public bool IsTelescopeDisplayEnabled { get; set; } = false;
    
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        if (Scoring.FurnSetIndex == Scoring.TelescopeRoomIndex)
        {
            CreateTelescope();
            _telescopeDisplay.GenerateConstellations();
        }
    }

    public void SetTelescopeRoomIndex()
    {
        Scoring.TelescopeRoomIndex = Random.Range(1, 5);
        Debug.Log(Scoring.TelescopeRoomIndex);
        Scoring.IsTelescopeIndexSet = true;
    }

    public void CreateTelescope()
    {
        Telescope telescope = Instantiate(_telescopePrefab);
        telescope.Init(_text, _hero, this);
    }
    
    public void EnableTelescopeDisplay()
    {
        IsTelescopeDisplayEnabled = true;

        CursorManager.Instance.ChangeCursorVisibility(false);
        _hero.StopReceivingInput = true;
        _telescopeDisplay.gameObject.SetActive(true);
    }

    public void DisableTelescopeDisplay()
    {
        IsTelescopeDisplayEnabled = false;

        CursorManager.Instance.ChangeCursorVisibility(true);
        _hero.StopReceivingInput = false;
        _telescopeDisplay.gameObject.SetActive(false);
    }
}
