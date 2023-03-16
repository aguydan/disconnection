using System.Collections;
using UnityEngine;

public class ActionItemManager : MonoBehaviour
{
    [SerializeField] ItemPanelButton _VRButton;
    [SerializeField] ItemPanelButton _bookButton;
    [SerializeField] ItemPanelButton _musicPlayerButton;
    [SerializeField] AIMPManager _AIMPManager;
    [SerializeField] UpperFingerButton _deactivateAIMPButton;
    [SerializeField] Sprite[] _disabledButtonSprites;
    [SerializeField] Sprite[] _enabledButtonSprites;
    
    public static ActionItemManager instance;

    public string _whoDid;
    public bool AlreadyClosed = false;
    public bool IsActionItemCreated = false;
    
    //FOR VRS
    public int numberOfVRs = 0;

    //FOR MUSIC PLAYER
    public int numberOfPlayers = 0;
    public int MusicPlayerItemTries = 2;
    public bool IsPlayerCompleted = false;
    public bool HasMusicPlayerStarted = false;

    //FOR BOOK
    private int _tries = 0;
    public int numberOfBooks = 0;
    private bool _isBookPopulated = false; 
    public bool IsBookVisible = false;

    private void Awake()
    {
        instance = this;
    }

    public void EnableActionItemButton(ActionItem.ActionItemType type)
    {
        switch (type)
        {
            case ActionItem.ActionItemType.VR:
            {
                _VRButton.gameObject.SetActive(true);
                numberOfVRs++;
                IsActionItemCreated = true;
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
                IsActionItemCreated = true;
            }
            break;
        }
    }

    public void WhoCreatedItem(string whoDid, bool status)
    {
        _whoDid = whoDid;
        
        Sprite[] currentButtonSprites = status ? _enabledButtonSprites : _disabledButtonSprites;
        
        if (whoDid == "book")
        {
            _VRButton.Button.interactable = status;
            _VRButton.Image.sprite = currentButtonSprites[0];

            _musicPlayerButton.Button.interactable = status;
            _musicPlayerButton.Image.sprite = currentButtonSprites[2];
        }
        else
        {
            _VRButton.Button.interactable = status;
            _VRButton.Image.sprite = currentButtonSprites[0];

            _bookButton.Button.interactable = status;
            _bookButton.Image.sprite = currentButtonSprites[1];

            _musicPlayerButton.Button.interactable = status;
            _musicPlayerButton.Image.sprite = currentButtonSprites[2];
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
        _tries++;
        _isBookPopulated = false;
        WhoCreatedItem("book", true);

        if (numberOfBooks == 0) _bookButton.gameObject.SetActive(false);

        BookItem.Instance.IsBookCompleted = false;
    }

    //MUSIC
    public void ActivateMusicPlayer()
    {
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
            IsActionItemCreated = false;
            HasMusicPlayerStarted = false;
            _AIMPManager.NextSongIndex();
        }
        WhoCreatedItem("musicPlayer", true);

        if (numberOfPlayers == 0) _musicPlayerButton.gameObject.SetActive(false);

        IsPlayerCompleted = false;
    }
}
