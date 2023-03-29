using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleCursorManager : MonoBehaviour
{
    [SerializeField] Texture2D[] cursorTexture;
    [SerializeField] FinaleHero _hero;

    public static FinaleCursorManager Instance;
    Vector2 grabHotspot;
    Vector2 fingerHotspot;
    Vector2 mousePosition;

    public bool StopAutomaticCursor = false;

    private void Awake()
    {
        Instance = this;
        fingerHotspot = new Vector2(110, 27);
        grabHotspot = new Vector2(cursorTexture[0].width / 2, cursorTexture[0].height / 2);
    }

    private void Start()
    {
        EnableCanGrabCursor();
    }

    private void Update()
    {
        HandleGrabbingChange();
    }

    void HandleGrabbingChange()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float distanceToHero = Vector2.Distance(_hero.transform.position, mousePosition);

        if (distanceToHero < 4)
        {
            EnableCanGrabCursor();
        }
        else
        {
            EnableCantGrabCursor();
        }
    }
    
    public void EnableCantGrabCursor()
    {
        Cursor.SetCursor(cursorTexture[0], grabHotspot, CursorMode.Auto);
    }

    public void EnableCanGrabCursor()
    {
        Cursor.SetCursor(cursorTexture[1], grabHotspot, CursorMode.Auto);
    }

    public void EnableFingerCursor()
    {
        Cursor.SetCursor(cursorTexture[2], fingerHotspot, CursorMode.Auto);
    }
}
