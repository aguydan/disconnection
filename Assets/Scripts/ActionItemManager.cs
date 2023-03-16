using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActionItemManager : MonoBehaviour
{
    [SerializeField] Image _VRButton;
    [SerializeField] Image _bookButton;
    [SerializeField] Image _musicPlayerButton;
    [SerializeField] PlayMusicPlayer _musicPlayer;
    [SerializeField] Image _deactivatePlayerButton;
    
    public static ActionItemManager instance;
    public bool _itemCreated = false;
    public string _whoDid;
    public bool AlreadyClosed = false;
    public int numberOfBooks = 0, numberOfPlayers = 0, numberOfVRs = 0, CurrentSong = -1;
    int _tries = 0;

    public bool IsPlayerCompleted = false;
    public bool HasMusicPlayerStarted = false;
    public int MusicPlayerItemTries = 2;
    
    [SerializeField] Sprite originalBook;
    [SerializeField] Sprite originalPlayer;
    [SerializeField] Sprite originalVR;

    private void Awake()
    {
        instance = this;
    }

    private void Start() {
        Sprite originalBook = _bookButton.sprite;
        Sprite originalPlayer = _musicPlayerButton.sprite;
        Sprite originalVR = _VRButton.sprite;
    }

    public void EnableActionItemButton(ActionItem.ActionItemType type)
    {
        switch (type)
        {
            case ActionItem.ActionItemType.VR:
                _VRButton.gameObject.SetActive(true);
                numberOfVRs++;
                _itemCreated = true;
            break;
            case ActionItem.ActionItemType.Book:
                _bookButton.gameObject.SetActive(true);
                numberOfBooks++;
            break;
            case ActionItem.ActionItemType.MusicPlayer:
                _musicPlayerButton.gameObject.SetActive(true);
                numberOfPlayers++;
                _itemCreated = true;
            break;
        }
    }

    public void WhoCreatedItem(string whoDid, bool status)
    {
        Sprite disabled = ItemSpawner.Instance.winningItem.look.sprite;
        _whoDid = whoDid;
        
        if (status)
        {
            switch (whoDid)
            {
                case "book":
                    _VRButton.GetComponent<Button>().interactable = status;
                    _VRButton.sprite = originalVR;
                    _musicPlayerButton.GetComponent<Button>().interactable = status;
                    _musicPlayerButton.sprite = originalPlayer;
                break;
                case "musicPlayer":
                    _VRButton.GetComponent<Button>().interactable = status;
                    _VRButton.sprite = originalVR;
                    _musicPlayerButton.GetComponent<Button>().interactable = status;
                    _musicPlayerButton.sprite = originalPlayer;
                    _bookButton.GetComponent<Button>().interactable = status;
                    _bookButton.sprite = originalBook;
                break;
                case "VR":
                    _bookButton.GetComponent<Button>().interactable = status;
                    _bookButton.sprite = originalVR;
                    _VRButton.GetComponent<Button>().interactable = status;
                    _VRButton.sprite = originalVR;
                    _musicPlayerButton.GetComponent<Button>().interactable = status;
                    _musicPlayerButton.sprite = originalPlayer;
                break;
            }
        }
        else
        {
            switch (whoDid)
            {
                case "book":
                    _VRButton.GetComponent<Button>().interactable = status;
                    _VRButton.sprite = disabled;
                    _musicPlayerButton.GetComponent<Button>().interactable = status;
                    _musicPlayerButton.sprite = disabled;
                break;
                case "musicPlayer":
                    _VRButton.GetComponent<Button>().interactable = status;
                    _VRButton.sprite = disabled;
                    _musicPlayerButton.GetComponent<Button>().interactable = status;
                    _musicPlayerButton.sprite = disabled;
                    _bookButton.GetComponent<Button>().interactable = status;
                    _bookButton.sprite = disabled;
                break;
                case "VR":
                    _bookButton.GetComponent<Button>().interactable = status;
                    _bookButton.sprite = disabled;
                    _VRButton.GetComponent<Button>().interactable = status;
                    _VRButton.sprite = disabled;
                    _musicPlayerButton.GetComponent<Button>().interactable = status;
                    _musicPlayerButton.sprite = disabled;
                break;
            }
        }
    }

    //BOOOK

    public void DeactivateBook()
    {
        BookItem.Instance.gameObject.SetActive(false);
        CursorManager.Instance.StopAutomaticCursor = false;

        if (BookItem.Instance.IsBookCompleted)
        {
            ItemSpawner.Instance.CompleteChallenge();

            numberOfBooks--;
            _tries++;
            _itemCreated = false;
            WhoCreatedItem("book", true);
        }
        if (numberOfBooks == 0) _bookButton.gameObject.SetActive(false);

        BookItem.Instance.IsBookCompleted = false;

        AlreadyClosed = true;
    }

    public void ActivateBook()
    {
        AlreadyClosed = false;
        
        if (!_itemCreated && numberOfBooks > 0)
        {
            BookItem.Instance.CreateAndPopulateGrids(_tries);
            _itemCreated = true;
            WhoCreatedItem("book", false);
        }
        BookItem.Instance.gameObject.SetActive(true);
        CursorManager.Instance.StopAutomaticCursor = true;
        CursorManager.Instance.EnableCanGrabCursor();
    }

    public IEnumerator DeactivateBookAutomatically()
    {
        yield return new WaitForSeconds(1);

        BookItem.Instance.gameObject.SetActive(false);
        CursorManager.Instance.StopAutomaticCursor = false;

        ItemSpawner.Instance.CompleteChallenge();

        numberOfBooks--;
        _tries++;
        _itemCreated = false;
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
        
        AlreadyClosed = false;
        
        WhoCreatedItem("musicPlayer", false);
        _musicPlayer.gameObject.SetActive(true);
        _deactivatePlayerButton.gameObject.SetActive(true);
    }

    public void DeactivateMusicPlayer()
    {
        _musicPlayer.gameObject.SetActive(false);
        _deactivatePlayerButton.gameObject.SetActive(false);

        if (IsPlayerCompleted)
        {
            ItemSpawner.Instance.CompleteChallenge();
            numberOfPlayers--;
            _itemCreated = false;
            HasMusicPlayerStarted = false;
        }
        WhoCreatedItem("musicPlayer", true);

        Debug.Log(numberOfPlayers);

        if (numberOfPlayers == 0) _musicPlayerButton.gameObject.SetActive(false);

        AlreadyClosed = true;
    }

    // public IEnumerator DeactivateMusicPlayerAutomatically()
    // {
    //     yield return new WaitForSeconds(1);

    //     _musicPlayer.gameObject.SetActive(false);
    //     _deactivatePlayerButton.gameObject.SetActive(false);

    //     ItemSpawner.Instance.CompleteChallenge();
    //     numberOfPlayers--;
    //     _itemCreated = false;
    //     WhoCreatedItem("musicPlayer", true);

    //     if (numberOfPlayers == 0) _musicPlayerButton.gameObject.SetActive(false);
    // }
}
