using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer look;
    public ItemSprite.ItemCategory Category;
    public CapsuleCollider2D capsuleCollider;
    [SerializeField] Animator animator;

    public bool hasPositivePoints = false;
    float distanceToHero;
    public Sprite OriginalSprite;

    private void Start()
    {
        OriginalSprite = look.sprite;
    }

    private void Update()
    {
        if (!(SceneManager.GetActiveScene().name == "Finale" || SceneManager.GetActiveScene().name == "BadEnding"))
        {
            distanceToHero = Vector2.Distance(transform.position, GameManager.instance.heroPosition);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (distanceToHero < 4)
        {
            animator.Play("ItemHover");
            SoundManager.Instance.PlayEffect(SoundManager.Instance.Effects[1], 0.25f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("New State");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (GameManager.instance.IsLevelCompleted)
        {
            Debug.Log("Come Forward!!!");
        }
        else
        {
            NotepadManager.Instance.UpdateNotepad(gameObject.GetComponent<Item>());
            
            if (hasPositivePoints && distanceToHero < 4)
            {
                SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[2]);
                Scoring.WinningItemSprites.Add(look.sprite);
                
                ScoreManager.instance.IncreaseEscapism(20);
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
                SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[2]);
                ScoreManager.instance.DecreaseEscapism();
                
                if (ActionItemManager.instance.HasMusicPlayerStarted)
                {
                    ActionItemManager.instance.MusicPlayerItemTries--;
                    if (ActionItemManager.instance.MusicPlayerItemTries == 0)
                    {
                        SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[9]);
                        ActionItemManager.instance.IsPlayerCompleted = true;
                        AIMPManager.Instance.Animator.LaunchExitAnimation();
                    }
                }

                if (ActionItemManager.instance.IsActionItemCurrentlyVisible)
                {
                    UIManager.instance.CallItemPopupNegative(OriginalSprite.name);
                }
                else
                {
                    UIManager.instance.CallItemPopupNegative(look.sprite.name);
                }

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
                    AIMPManager.Instance.Animator.LaunchExitAnimation();
                break;
                case "VR":
                    ActionItemManager.instance.IsVRCompleted = true;
                    ActionItemManager.instance.AIMDeactivateVR();
                break;
            }
    }
}
