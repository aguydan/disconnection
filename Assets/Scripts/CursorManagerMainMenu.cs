using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagerMainMenu : MonoBehaviour
{
    [SerializeField] Texture2D cursorTexture;
    
    Vector2 fingerHotspot;

    private void Awake()
    {
        fingerHotspot = new Vector2(110, 27);
        EnableFingerCursor();
    }

    void EnableFingerCursor()
    {
        Cursor.SetCursor(cursorTexture, fingerHotspot, CursorMode.Auto);
    }
}
