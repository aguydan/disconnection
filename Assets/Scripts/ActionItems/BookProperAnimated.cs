using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookProperAnimated : MonoBehaviour
{
    [SerializeField] GameObject _bookBackground;
    
    public void ActivateBookFromAnimation()
    {
        ActionItemManager.instance.ActivateBook();
    }

    public void DeactivateBookProper()
    {
        _bookBackground.SetActive(false);
        gameObject.SetActive(false);
    }
}
