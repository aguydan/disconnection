using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AIM = ActionItemManager;
using PostType = SocialMediaManager.PostType;

public class SocialMediaPost : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PostType Type { get; set; }
    public Image Image;

    public void EnablePostEffect()
    {
        //МОЖЕТ ОТДЕЛЬНЫЕ ПОСТЫ ДАЮТ РАЗНЫЕ ЭФФЕКТЫ ТИПА ЭСКАПИЗМА
        //МОЖЕТ НАДО СДЕЛАТЬ ПОПАП КОТОРЫЙ ПРОСТО ПРОДУБЛИРУЕТ ПОСТ И ПОКАЖЕТ ЭФФЕКТ
        //УВЕЛИЧИВАТЬ МУД СКОР!!!!
        
        switch (Type)
        {
            case PostType.Positive:
                AIM.instance.SMImpact.text += "+";
                AIM.instance.SMTries--;
            break;
            case PostType.Negative:
                AIM.instance.SMImpact.text += "-";
                AIM.instance.SMTries--;
            break;
            case PostType.Secret: //СДЕЛАТЬ
            break;
        }

        if (AIM.instance.SMTries == 0)
        {
            //deactivate from post
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = true;

        CursorManager.Instance.EnableFingerCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.EnableCanGrabCursor();
    }
}
