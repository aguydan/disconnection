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
                // Scoring.WinningItems.Add(gameObject.GetComponent<Item>());
                
                ScoreManager.instance.IncreaseScore();
                if (ActionItemManager.instance.IsActionItemCreated)
                {
                    WhatActionItemToDeactivate(ActionItemManager.instance._whoDid);
                }
                UIManager.instance.CallItemPopupPositive(look.sprite.name);
                GameManager.instance.SpawnDoorToNextLevel();

                Destroy(gameObject);
            }
            else if (distanceToHero < 4)
            {
                ScoreManager.instance.DecreaseScore();
                
                if (ActionItemManager.instance.HasMusicPlayerStarted)
                {
                    ActionItemManager.instance.MusicPlayerItemTries--;

                    if (ActionItemManager.instance.MusicPlayerItemTries == 0)
                    {
                        ActionItemManager.instance.IsPlayerCompleted = true;
                        ActionItemManager.instance.DeactivateMusicPlayer();
                    }
                }

                UIManager.instance.CallItemPopupNegative(look.sprite.name);

                Destroy(gameObject);
            }
        }
    }

    public void WhatActionItemToDeactivate(string name)
    {
            switch (name)
            {
                case "musicPlayer":
                    ActionItemManager.instance.IsPlayerCompleted = true;
                    ActionItemManager.instance.DeactivateMusicPlayer();
                break;
                case "VR":
                    ActionItemManager.instance.IsVRCompleted = true;
                    ActionItemManager.instance.AIMDeactivateVR();
                break;
            }
    }
}
