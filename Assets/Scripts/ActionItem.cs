using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator _animator;
    
    public enum ActionItemType {
        VR,
        Book,
        MusicPlayer
    }
    public ActionItemType _actionItemType;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.instance.IsLevelCompleted)
        {
            Debug.Log("Come Forward!!!");
        }
        else
        {
            SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[4]);
            ActionItemManager.instance.PickUpActionItem(_actionItemType);

            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.Play("ItemHover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.Play("New State");
    }
}
