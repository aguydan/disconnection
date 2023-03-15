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
        if (GameManager.instance.IsLevelCompleted)
        {
            Debug.Log("Come Forward!!!");
        }
        else
        {
            ActionItemManager.instance.EnableActionItemButton(_actionItemType);
            ActionItemManager.instance.numberOfBooks++; //not all the items are books

            Debug.Log(ActionItemManager.instance.numberOfBooks);

            Destroy(gameObject);
        }
    }
}
