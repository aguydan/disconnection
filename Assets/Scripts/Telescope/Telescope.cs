using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Telescope : MonoBehaviour
{
    private TelescopeManager _manager;
    private TextMeshProUGUI _text;
    
    private Hero _hero;
    private bool _isInRange = false;
    private float _distanceToPlayer;

    public void Init(TextMeshProUGUI text, Hero hero, TelescopeManager manager)
    {
        _text = text;
        _hero = hero;
        _manager = manager;

        _text.gameObject.SetActive(true);
    }
    
    private void Update()
    {
        _distanceToPlayer = Vector2.Distance(transform.position, _hero.transform.position);

        if (_distanceToPlayer < 4)
        {
            _isInRange = true;
            _text.alpha = 1;
        }
        else if (_distanceToPlayer >= 4)
        {
            _isInRange = true;
            _text.alpha = 1 - (_distanceToPlayer / 6);
        }
        else
        {
            _isInRange = false;
            _text.alpha = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.T) && _isInRange)
        {
            if (!_manager.IsTelescopeDisplayEnabled)
            {
                _manager.EnableTelescopeDisplay();
            }
            else
            {
                _manager.DisableTelescopeDisplay();
            }
        }
    }
}
