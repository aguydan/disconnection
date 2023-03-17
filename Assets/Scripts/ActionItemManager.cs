using System.Collections;
using UnityEngine;

public class ActionItemManager : MonoBehaviour
{
    [SerializeField] ItemPanelButton _VRButton;
    [SerializeField] ItemPanelButton _bookButton;
    [SerializeField] ItemPanelButton _musicPlayerButton;
    [SerializeField] AIMPManager _AIMPManager;
    [SerializeField] VRManager _VRManager;
    [SerializeField] UpperFingerButton _deactivateAIMPButton;
    [SerializeField] Sprite[] _disabledButtonSprites;
    [SerializeField] Sprite[] _enabledButtonSprites;
    
    public static ActionItemManager instance;

    public string _whoDid;
    public bool AlreadyClosed = false;
    public bool IsActionItemCreated = false;

    //FOR MUSIC PLAYER
    int numberOfPlayers = 0;
    public int MusicPlayerItemTries = 2;
    public bool IsPlayerCompleted = false;
    public bool HasMusicPlayerStarted = false;

    //FOR BOOK
    private int _tries = 0;
    int numberOfBooks = 0;
    private bool _isBookPopulated = false; 
    public bool IsBookVisible = false;

    private void Awake()
    {
        instance = this;
    }

    public void PickUpActionItem(ActionItem.ActionItemType type)
    {
        switch (type)
        {
            case ActionItem.ActionItemType.VR:
            {
                _VRButton.gameObject.SetActive(true);
                _VRManager.AmountOfVRs++;
            }
            break;
            case ActionItem.ActionItemType.Book:
            {
                _bookButton.gameObject.SetActive(true);
                numberOfBooks++;
            }
            break;
            case ActionItem.ActionItemType.MusicPlayer:
            {
                _musicPlayerButton.gameObject.SetActive(true);
                numberOfPlayers++;
            }
            break;
        }
        UpdateAmountOfActionItems();
    }

    void UpdateAmountOfActionItems()
    {
        if (_VRManager.AmountOfVRs == 2) _VRButton.X2.gameObject.SetActive(true);
        if (numberOfPlayers == 2) _musicPlayerButton.X2.gameObject.SetActive(true);
        if (numberOfBooks == 2) _bookButton.X2.gameObject.SetActive(true);

        if (_VRManager.AmountOfVRs < 2) _VRButton.X2.gameObject.SetActive(false);
        if (numberOfPlayers < 2) _musicPlayerButton.X2.gameObject.SetActive(false);
        if (numberOfBooks < 2) _bookButton.X2.gameObject.SetActive(false);
    }

    public void WhoCreatedItem(string whoDid, bool status)
    {
        _whoDid = whoDid;
        
        Sprite[] currentButtonSprites = status ? _enabledButtonSprites : _disabledButtonSprites;
        
        _VRButton.Button.interactable = status;
        _VRButton.Image.sprite = currentButtonSprites[0];

        _musicPlayerButton.Button.interactable = status;
        _musicPlayerButton.Image.sprite = currentButtonSprites[2];

        if (whoDid != "book")
        {
            _bookButton.Button.interactable = status;
            _bookButton.Image.sprite = currentButtonSprites[1];
        }
    }

    //BOOOK
    public void ActivateBook()
    {
        IsBookVisible = true;
        AlreadyClosed = false;
        
        if (!_isBookPopulated && numberOfBooks > 0)
        {
            BookItem.Instance.CreateAndPopulateGrids(_tries);
            _isBookPopulated = true;
            WhoCreatedItem("book", false);
        }
        BookItem.Instance.gameObject.SetActive(true);
        CursorManager.Instance.StopAutomaticCursor = true;
        CursorManager.Instance.EnableCanGrabCursor();
    }

    public void DeactivateBook()
    {
        IsBookVisible = false;
        
        BookItem.Instance.gameObject.SetActive(false);
        CursorManager.Instance.StopAutomaticCursor = false;

        if (BookItem.Instance.IsBookCompleted)
        {
            ItemSpawner.Instance.CompleteChallenge();

            numberOfBooks--;
            UpdateAmountOfActionItems();
            _tries++;
            _isBookPopulated = false;
            WhoCreatedItem("book", true);
        }
        if (numberOfBooks == 0) _bookButton.gameObject.SetActive(false);

        BookItem.Instance.IsBookCompleted = false;

        AlreadyClosed = true;
    }

    public IEnumerator DeactivateBookAutomatically()
    {
        yield return new WaitForSeconds(1);

        IsBookVisible = false;
        
        BookItem.Instance.gameObject.SetActive(false);
        CursorManager.Instance.StopAutomaticCursor = false;

        ItemSpawner.Instance.CompleteChallenge();

        numberOfBooks--;
        UpdateAmountOfActionItems();
        _tries++;
        _isBookPopulated = false;
        WhoCreatedItem("book", true);

        if (numberOfBooks == 0) _bookButton.gameObject.SetActive(false);

        BookItem.Instance.IsBookCompleted = false;
    }

    //MUSIC
    public void ActivateMusicPlayer()
    {
        IsActionItemCreated = true;
        
        if (!HasMusicPlayerStarted)
        {
            HasMusicPlayerStarted = true;
            MusicPlayerItemTries = 2;
        }
        
        WhoCreatedItem("musicPlayer", false);
        _AIMPManager.PlayVolumeControlSong();
        _deactivateAIMPButton.gameObject.SetActive(true);
    }

    public void DeactivateMusicPlayer()
    {
        _AIMPManager.StopVolumeControlSong();
        _deactivateAIMPButton.gameObject.SetActive(false);

        if (IsPlayerCompleted)
        {
            ItemSpawner.Instance.CompleteChallenge();
            numberOfPlayers--;
            UpdateAmountOfActionItems();
            IsActionItemCreated = false;
            HasMusicPlayerStarted = false;
            _AIMPManager.NextSongIndex();
        }
        WhoCreatedItem("musicPlayer", true);

        if (numberOfPlayers == 0) _musicPlayerButton.gameObject.SetActive(false);

        IsPlayerCompleted = false;
    }
}
