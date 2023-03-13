using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionItemManager : MonoBehaviour
{
    [SerializeField] Image _VRButton;
    [SerializeField] Image _bookButton;
    [SerializeField] Image _musicPlayerButton;
    
    public static ActionItemManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    public void EnableActionItemButton(ActionItem.ActionItemType type)
    {
        switch (type)
        {
            case ActionItem.ActionItemType.VR:
                _VRButton.gameObject.SetActive(true);
            break;
            case ActionItem.ActionItemType.Book:
                _bookButton.gameObject.SetActive(true);
            break;
            case ActionItem.ActionItemType.MusicPlayer:
                _musicPlayerButton.gameObject.SetActive(true);
            break;
        }
    }
}
