using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FingerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = true;
        
        CursorManager.Instance.EnableFingerCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = false;
    }
}

