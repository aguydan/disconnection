using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PostType = SocialMediaManager.PostType;

public class SocialMediaPost : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PostType Type { get; private set; }
    private Sprite _sprite;
    private bool _isPositiveWinnig = false;

    private SocialMediaManager _manager;

    public void Init(PostType type, Sprite sprite, SocialMediaManager manager, bool isPositiveWinning = false)
    {
        Type = type;

        GetComponent<Image>().sprite = sprite;
        _sprite = sprite;

        _isPositiveWinnig = isPositiveWinning;

        _manager = manager;
    }

    public void OnPostClick()
    {
        switch (Type)
        {
            case PostType.Positive:
                if (_isPositiveWinnig)
                {
                    ScoreManager.instance.IncreaseMood(1);
                }
                else
                {
                    ScoreManager.instance.IncreaseEscapism(5);
                }

                _manager.ImpactSigns.text += "+";
                _manager.ImpactSigns.GetComponent<Animator>().Play("Score Sign");
                _manager.Tries--;
            break;
            case PostType.Negative:
                _manager.ImpactSigns.text += "-";
                _manager.ImpactSigns.GetComponent<Animator>().Play("Score Sign");
                _manager.Tries--;
            break;
            case PostType.Secret:
                _manager.ChangeSecretPostState(true);
            break;
        }

        if (_manager.Tries == 0)
        {
            StartCoroutine(_manager.FinishSocialMedia());
        }

        GetComponent<Button>().interactable = false;
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
