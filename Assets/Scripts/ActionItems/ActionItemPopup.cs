using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ActionItemPopup : MonoBehaviour
{
    [SerializeField] Image _actionItemImage;
    [SerializeField] TextMeshProUGUI _youGotItemText;
    [SerializeField] Sprite[] _actionItemSprites;

    public void WhichActionItemToSpawn()
    {
        Dictionary<string, int> amountOfActionItems = ActionItemSpawner.Instance.AmountOfActionItems;

        if (amountOfActionItems["book"] == 2)
        {
            AddMusicPlayer();
        }
        else if (amountOfActionItems["musicPlayer"] == 2)
        {
            AddBook();
        }
        else
        {
            int randomNum = Random.Range(0, 2);

            if (randomNum == 0)
            {
                AddMusicPlayer();
            }
            else
            {
                AddBook();
            }
        }
    }

    void AddMusicPlayer()
    {
        _actionItemImage.sprite = _actionItemSprites[0];
        ActionItemManager.instance.PickUpActionItem(ActionItem.ActionItemType.MusicPlayer);
    }

    void AddBook()
    {
        _actionItemImage.sprite = _actionItemSprites[1];
        ActionItemManager.instance.PickUpActionItem(ActionItem.ActionItemType.Book);
    }
    
    public void DeactivateActionItemPopup()
    {
        gameObject.SetActive(false);
        UIManager.instance.IsPopupActive = false;

        ActionItemManager.instance.IsVRCompleted = true;
        ActionItemManager.instance.AIMDeactivateVR();
    }
}
