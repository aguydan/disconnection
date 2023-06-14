using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelButton : MonoBehaviour
{
    [SerializeField] GameObject _bookBackground;
    [SerializeField] GameObject _bookProper;
    
    public Button Button;
    public Image Image;
    public Image X2;

    public void ActivateBookProper()
    {
        SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[10]);
        _bookBackground.SetActive(true);
        _bookProper.SetActive(true);
    }
}
