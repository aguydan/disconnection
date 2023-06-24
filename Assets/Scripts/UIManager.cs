using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] ItemPopup itemPopup;
   [SerializeField] ActionItemPopup _actionItemPopup;

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
        bool isAffectingMood = false;
        
        if (Random.Range(0, 4) == 0)
        {
            isAffectingMood = true;
            ScoreManager.instance.DecreaseMood(2);
        }
        
        itemPopup.UpdateItemPopupNegative(key, isAffectingMood);
        itemPopup.gameObject.SetActive(true);
        IsPopupActive = true;
    }

    public void CallActionItemPopup()
    {
        _actionItemPopup.WhichActionItemToSpawn();
        _actionItemPopup.gameObject.SetActive(true);
        IsPopupActive = true;
    }
}
