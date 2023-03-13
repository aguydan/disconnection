using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] ItemPopup itemPopup;

    public static UIManager instance;
    public bool IsPopupActive = false;

    private void Awake()
    {
        instance = this;
    }

    public void CallItemPopupPositive(string key)
    {
        itemPopup.UpdateItemPopupPositive(key);
        itemPopup.gameObject.SetActive(true);
        IsPopupActive = true;
    }

    public void CallItemPopupNegative(string key)
    {
        itemPopup.UpdateItemPopupNegative(key);
        itemPopup.gameObject.SetActive(true);
        IsPopupActive = true;
    }
}
