using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPopup : MonoBehaviour
{
    [SerializeField] Image _itemImage;
    [SerializeField] Image _popupBody;
    [SerializeField] GameObject _additionalEffect;
    [SerializeField] Sprite[] _possibleBodies;
    [SerializeField] TextMeshProUGUI _itemText;
    [SerializeField] ItemObjects itemObjects;

    ItemObject _currentItemObject;

    public void UpdateItemPopupPositive(string key)
    {
        _additionalEffect.SetActive(false);
        _popupBody.sprite = _possibleBodies[0];
        
        foreach (ItemObject itemObject in itemObjects.PositiveItemObjects)
        {
            if (itemObject.ItemKey == key)
            {
                _currentItemObject = itemObject;
                break;
            }
        }

        _itemImage.sprite = _currentItemObject.ItemSprite;

        _itemImage.SetNativeSize();
        _itemImage.preserveAspect = true;
        _itemImage.rectTransform.sizeDelta = new Vector2(_itemImage.rectTransform.sizeDelta.x, 120);
        
        _itemText.text = _currentItemObject.ItemText;
    }

    public void UpdateItemPopupNegative(string key, bool isAffectingMood)
    {
        if (isAffectingMood)
        {
            _popupBody.sprite = _possibleBodies[2];
            _additionalEffect.SetActive(true);

            // foreach (ItemObject itemObject in itemObjects.ExtraNegativeItemObjects)
            // {
            //     if (itemObject.ItemKey == key)
            //     {
            //         _currentItemObject = itemObject;
            //         break;
            //     }
            // }
        }
        else
        {
            _popupBody.sprite = _possibleBodies[1];
            _additionalEffect.SetActive(false);

            // foreach (ItemObject itemObject in itemObjects.NegativeItemObjects)
            // {
            //     if (itemObject.ItemKey == key)
            //     {
            //         _currentItemObject = itemObject;
            //         break;
            //     }
            // }
        }
        
        //replace this
        foreach (ItemObject itemObject in itemObjects.NegativeItemObjects)
        {
            if (itemObject.ItemKey == key)
            {
                _currentItemObject = itemObject;
                break;
            }
        }

        _itemImage.sprite = _currentItemObject.ItemSprite;

        _itemImage.SetNativeSize();
        _itemImage.preserveAspect = true;
        _itemImage.rectTransform.sizeDelta = new Vector2(_itemImage.rectTransform.sizeDelta.x, 120);

        _itemText.text = _currentItemObject.ItemText;
    }

    public void DeactivatePopup()
    {
        gameObject.SetActive(false);
        UIManager.instance.IsPopupActive = false;
    }
}
