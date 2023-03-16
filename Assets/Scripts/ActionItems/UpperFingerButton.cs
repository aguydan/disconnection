using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpperFingerButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = true;
        
        CursorManager.Instance.EnableFingerCursor();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = false;
    }
}
