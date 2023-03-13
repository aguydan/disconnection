using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionItem : MonoBehaviour, IPointerDownHandler
{
    public enum ActionItemType {
        VR,
        Book,
        MusicPlayer
    }
    public ActionItemType _actionItemType;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("you equipped " + _actionItemType);
        ActionItemManager.instance.EnableActionItemButton(_actionItemType);

        Destroy(gameObject);
    }
}