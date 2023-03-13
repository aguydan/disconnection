using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public SpriteRenderer look;
    public CapsuleCollider2D capsuleCollider;
    Animator animator;
    public bool hasPositivePoints = false;
    float distanceToHero;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distanceToHero = Vector2.Distance(transform.position, GameManager.instance.heroPosition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.Play("ItemHover");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (hasPositivePoints && distanceToHero < 4)
        {
            Debug.Log("win, win");
            ScoreManager.instance.IncreaseScore();
            UIManager.instance.CallItemPopupPositive(look.sprite.name);
            GameManager.instance.SpawnDoorToNextLevel();

            Destroy(gameObject);
        } else if (distanceToHero < 4) {
            Debug.Log("lost, minus points to griffindor");
            ScoreManager.instance.DecreaseScore();
            UIManager.instance.CallItemPopupNegative(look.sprite.name);

            Destroy(gameObject);
        }
    }
}
