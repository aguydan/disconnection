using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionItemManager : MonoBehaviour
{
    [SerializeField] ItemPanelButton _VRButton;
    [SerializeField] ItemPanelButton _bookButton;
    [SerializeField] ItemPanelButton _musicPlayerButton;
    [SerializeField] ItemPanelButton _SMButton;
    [SerializeField] AIMPManager _AIMPManager;
    [SerializeField] VRManager _VRManager;
    [SerializeField] UpperFingerButton _deactivateAIMPButton;
    [SerializeField] Sprite[] _disabledButtonSprites;
    [SerializeField] Sprite[] _enabledButtonSprites;
    [SerializeField] Animator _bookProperAnimator;
    [SerializeField] GameObject _bookBackground;

    [SerializeField] GameObject _MYR;
    [SerializeField] GameObject _VRMask;
    [SerializeField] Animator _VRBackground;
    [SerializeField] Animator _VR;
    [SerializeField] GameObject _VRGuard;
    
    public static ActionItemManager instance;

    public string _whoDid;
    public bool AlreadyClosed = false; //нужен ли если есть stop coroutine?
    public bool IsActionItemCreated = false;
    public bool IsActionItemCurrentlyVisible = false;

    //FOR VR
    public bool IsVRCompleted = false;

    //FOR SOCIAL MEDIA
    public bool IsSMCompleted = false;
    public TextMeshProUGUI SMImpact;
    public int SMTries = 3;
    
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
            case ActionItem.ActionItemType.VR: _VRManager.PickUpVR();
            break;
            case ActionItem.ActionItemType.SocialMedia: SocialMediaManager.Instance.PickUpSocialMedia();
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

    void PanelButtonDisabler(string lastAddedItem)
    {
        _whoDid = lastAddedItem;
        
        Dictionary<string, ItemPanelButton> buttons = new Dictionary<string, ItemPanelButton>()
        {
            { "VR", _VRButton },
            { "musicPlayer", _musicPlayerButton },
            { "book", _bookButton },
            { "SM", _SMButton }
        };
        Dictionary<string, int> spriteIndexes = new Dictionary<string, int>()
        {
            { "VR", 0 },
            { "musicPlayer", 1 },
            { "book", 2 },
            { "SM", 3 }
        };

        if (IsActionItemCreated)
        {
            foreach (KeyValuePair<string, ItemPanelButton> button in buttons)
            {
                button.Value.Button.interactable = false;
                button.Value.Image.sprite = _disabledButtonSprites[spriteIndexes[button.Key]];
            }

            if (!IsActionItemCurrentlyVisible)
            {
                buttons[lastAddedItem].Button.interactable = true;
                buttons[lastAddedItem].Image.sprite = _enabledButtonSprites[spriteIndexes[lastAddedItem]];
            }
        }
        else
        {
            foreach (KeyValuePair<string, ItemPanelButton> button in buttons)
            {
                button.Value.Button.interactable = true;
                button.Value.Image.sprite = _enabledButtonSprites[spriteIndexes[button.Key]];
            }
        }
    }

    //BOOOK
    public void ActivateBook()
    {
        IsActionItemCreated = true;
        IsBookVisible = true;
        AlreadyClosed = false;
        
        if (!_isBookPopulated && numberOfBooks > 0)
        {
            BookItem.Instance.CreateAndPopulateGrids(_tries);
            _isBookPopulated = true;
        }
        IsActionItemCurrentlyVisible = true;
        PanelButtonDisabler("book");

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
            IsActionItemCreated = false;
        }
        IsActionItemCurrentlyVisible = false;
        PanelButtonDisabler("book");

        if (numberOfBooks == 0) _bookButton.gameObject.SetActive(false);

        BookItem.Instance.IsBookCompleted = false;
        AlreadyClosed = true;

        PlayBookCloseAnimation();

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
        IsActionItemCreated = false;
        IsActionItemCurrentlyVisible = false;
        PanelButtonDisabler("book");

        if (numberOfBooks == 0) _bookButton.gameObject.SetActive(false);

        BookItem.Instance.IsBookCompleted = false;

        PlayBookCloseAnimation();
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
        
        IsActionItemCurrentlyVisible = true;
        PanelButtonDisabler("musicPlayer");
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
        IsActionItemCurrentlyVisible = false;
        PanelButtonDisabler("musicPlayer");

        if (numberOfPlayers == 0) _musicPlayerButton.gameObject.SetActive(false);

        IsPlayerCompleted = false;
    }

    //VR
    public void AIMActivateVR()
    {
        _VRGuard.SetActive(true);
        IsActionItemCreated = true;
        
        _VRManager.ActivateVR();
        PanelButtonDisabler("VR");
    }

    public void AIMDeactivateVR()
    {
        if (IsVRCompleted)
        {
            _VRManager.AmountOfVRs--;
            UpdateAmountOfActionItems();
            IsActionItemCreated = false;
        }

        _VRManager.DeactivateVR();
        PanelButtonDisabler("VR");

        PlayVRCloseAnimation();
    }

    void PlayVRCloseAnimation()
    {
        _VRGuard.SetActive(true);
        _MYR.SetActive(false);
        _VRMask.SetActive(false);
        _VRBackground.gameObject.SetActive(true);

        _VRBackground.Play("VRBackgroundOff");
        _VR.Play("VROff");
    }

    void PlayBookCloseAnimation()
    {
        _bookProperAnimator.Play("BookClose");
        SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[10]);
    }

    //SOCIAL MEDIA
    public void SMMActivateSocialMedia()
    {
        IsActionItemCreated = true;

        SocialMediaManager.Instance.ActivateSocialMedia();
        PanelButtonDisabler("SM");
    }

    public void SMMDeactivateSocialMedia()
    {
        if (IsSMCompleted)
        {
            IsActionItemCreated = false;
        }

        SocialMediaManager.Instance.DeactivateSocialMedia();
        PanelButtonDisabler("SM");
    }
}
