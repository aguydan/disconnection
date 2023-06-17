using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Comment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SocialMediaManager _manager;
    public bool IsWinning { get; private set; } = false;
    
    public void Init(Sprite sprite, SocialMediaManager manager, bool isWinning = false)
    {
        GetComponent<Image>().sprite = sprite;
        IsWinning = isWinning;
        _manager = manager;
    }

    public void OnCommentClick()
    {
        if (IsWinning)
        {
            _manager.ImpactSigns.text += "+";
        }
        else
        {
            _manager.ImpactSigns.text += "-";
        }

        _manager.Tries--;
        _manager.ChangeSecretPostState(false);

        if (_manager.Tries == 0)
        {
            _manager.FinishSocialMedia();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.EnableFingerCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.EnableCanGrabCursor();
    }
}
