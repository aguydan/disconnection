using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{
    [SerializeField] Hero hero;

    bool IsInRange = false;
    float _distanceToPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsInRange)
        {
            hero.Animator.Play("CharZen");
        }
    }
}
