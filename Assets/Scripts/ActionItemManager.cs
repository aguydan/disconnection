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
    bool _bookCreated = false;
    public bool AlreadyClosed = false;
    public int numberOfBooks = 0;
    int _tries = 0;
    
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

    public void DeactivateBook()
    {
        BookItem.Instance.gameObject.SetActive(false);

        if (BookItem.Instance.IsBookCompleted)
        {
            ItemSpawner.Instance.CompleteChallenge();

            numberOfBooks--;
            _tries++;
            _bookCreated = false;
        }
        if (numberOfBooks == 0) _bookButton.gameObject.SetActive(false);

        BookItem.Instance.IsBookCompleted = false;

        AlreadyClosed = true;
    }

    public void ActivateBook()
    {
        AlreadyClosed = false;
        
        if (!_bookCreated && numberOfBooks > 0)
        {
            BookItem.Instance.CreateAndPopulateGrids(_tries);
            _bookCreated = true;
        }
        BookItem.Instance.gameObject.SetActive(true);
    }

    public IEnumerator DeactivateBookAutomatically()
    {
        yield return new WaitForSeconds(1);

        BookItem.Instance.gameObject.SetActive(false);
        ItemSpawner.Instance.CompleteChallenge();

        numberOfBooks--;
        _tries++;
        _bookCreated = false;

        if (numberOfBooks == 0) _bookButton.gameObject.SetActive(false);

        BookItem.Instance.IsBookCompleted = false;
    }
}
