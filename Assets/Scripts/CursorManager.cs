using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D[] cursorTexture;

    public static CursorManager Instance;
    Vector2 grabHotspot;
    Vector2 fingerHotspot;
    Vector2 mousePosition;
    float distanceToHero;

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
        if (UIManager.instance.IsPopupActive)
        {
            EnableFingerCursor();
        }
        else if (StopAutomaticCursor)
        {
            return;
        }
        else
        {
            HandleGrabbingChange();
        }
    }

    void HandleGrabbingChange()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        distanceToHero = Vector2.Distance(GameManager.instance.heroPosition, mousePosition);

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

    public void ChangeCursorVisibility(bool isVisible)
    {
        if (!isVisible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
