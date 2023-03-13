using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public SpriteRenderer look;
    public CapsuleCollider2D capsuleCollider;
    [SerializeField] Animator animator;

    public bool hasPositivePoints = false;
    float distanceToHero;

    private void Update()
    {
        distanceToHero = Vector2.Distance(transform.position, GameManager.instance.heroPosition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (distanceToHero < 4) animator.Play("ItemHover");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (GameManager.instance.IsLevelCompleted)
        {
            Debug.Log("Come Forward!!!");
        }
        else
        {
            if (hasPositivePoints && distanceToHero < 4)
            {
                ScoreManager.instance.IncreaseScore();
                UIManager.instance.CallItemPopupPositive(look.sprite.name);
                GameManager.instance.SpawnDoorToNextLevel();

                Destroy(gameObject);
            }
            else if (distanceToHero < 4)
            {
                ScoreManager.instance.DecreaseScore();
                UIManager.instance.CallItemPopupNegative(look.sprite.name);

                Destroy(gameObject);
            }
        }
    }
}
