using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Carpet : MonoBehaviour
{
    [SerializeField] FinaleHero _hero;
    [SerializeField] TextMeshProUGUI _text;

    bool IsInRange = false;
    float _distanceToPlayer;

    private void Update()
    {
        _distanceToPlayer = Vector2.Distance(transform.position, _hero.transform.position);

        if (_distanceToPlayer < 2)
        {
            IsInRange = true;
            _text.alpha = 1 - (_distanceToPlayer / 2);
        }
        else
        {
            _text.alpha = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && IsInRange)
        {
            FinaleGameManager.Instance.StopPlayerInput = true;
            _hero.Animator.Play("CharZen");
        }
    }
}
