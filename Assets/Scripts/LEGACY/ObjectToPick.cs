using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPick : MonoBehaviour
{
    float distanceToHero;
    SpriteRenderer look;
    Hero hero;
    bool isHolding = false;
    
    void Start()
    {
        look = GetComponent<SpriteRenderer>();
        hero = FindObjectOfType<Hero>();
    }

    void Update()
    {
        distanceToHero = Vector2.Distance(transform.position, hero.transform.position);

        if (distanceToHero < 4)
        {
            look.color = Color.red;
            if (Input.GetKeyDown("e")) isHolding = !isHolding;
        } 
        else 
        {
            look.color = Color.white;
        }

        if (isHolding) {
            transform.position = hero.transform.position;
            transform.parent = hero.transform; //?
        }
    }
}
