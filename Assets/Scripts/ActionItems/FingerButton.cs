using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FingerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = true;
        
        CursorManager.Instance.EnableFingerCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!ActionItemManager.instance.IsBookVisible)
        {
            CursorManager.Instance.StopAutomaticCursor = false;

            CursorManager.Instance.EnableCanGrabCursor();
        }
    }
}

