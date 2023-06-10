using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotepadButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        NotepadManager.Instance.IsNotepadVisible = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = true;
        
        CursorManager.Instance.EnableFingerCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!NotepadManager.Instance.IsNotepadVisible)
        {
            CursorManager.Instance.StopAutomaticCursor = false;

            CursorManager.Instance.EnableCanGrabCursor();
        }
    }
}
