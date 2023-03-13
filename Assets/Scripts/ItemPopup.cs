using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPopup : MonoBehaviour
{
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshProUGUI _itemText;
    [SerializeField] ItemObjects itemObjects;

    ItemObject _currentItemObject;

    public void UpdateItemPopupPositive(string key)
    {
        foreach (ItemObject itemObject in itemObjects.PositiveItemObjects)
        {
            if (itemObject.ItemKey == key)
            {
                _currentItemObject = itemObject;
                Debug.Log(itemObject.ItemKey + " " + key);
                break;
            }
        }

        _itemImage.sprite = _currentItemObject.ItemSprite;
        _itemText.text = _currentItemObject.ItemText;
    }

    public void UpdateItemPopupNegative(string key)
    {
        foreach (ItemObject itemObject in itemObjects.NegativeItemObjects)
        {
            if (itemObject.ItemKey == key)
            {
                _currentItemObject = itemObject;
                break;
            }
        }

        _itemImage.sprite = _currentItemObject.ItemSprite;
        _itemText.text = _currentItemObject.ItemText;
    }

    public void DeactivatePopup()
    {
        gameObject.SetActive(false);
    }
}
