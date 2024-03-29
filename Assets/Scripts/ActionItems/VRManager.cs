using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VRManager : MonoBehaviour
{
    [SerializeField] private VRItem _VR;
    [SerializeField] private Button _VRButton;
    [SerializeField] private Button _upperVRButton;

    [SerializeField] private Volume _volume;
    [SerializeField] private VolumeProfile _mainProfile;
    
    public int AmountOfVRs = 0;
    bool _hasVRStarted = false;
    int _wrongItemsAmount;
    Coroutine _lastCoroutine;

    public void ActivateVR()
    {
        SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[5]);
        _VR.gameObject.SetActive(true);
        _VR.UpdateInteractableColliders(true);
        _upperVRButton.gameObject.SetActive(true);

        if (!_hasVRStarted)
        {
            _wrongItemsAmount = _VR.CalculateStartingWrongItemsAmount();
            _VR.IdentifyWrongItems(_wrongItemsAmount);
            _hasVRStarted = true;
        }

        _VR.UpdateWrongItems();
        _lastCoroutine = StartCoroutine(_VR.TurnItemsToHintItems());
        ActionItemManager.instance.IsActionItemCurrentlyVisible = true;
    }

    public void DeactivateVR()
    {
        if (_lastCoroutine != null) StopCoroutine(_lastCoroutine);
        SoundManager.Instance.StopEffects();
        _VR.UpdateWrongItems();
        _VR.DecreaseWrongItemsSortingOrder();
        _VR.TurnItemsBack();
        _volume.profile = _mainProfile;
        
        _VR.UpdateInteractableColliders(false);
        _upperVRButton.gameObject.SetActive(false);

        if (AmountOfVRs == 0) _VRButton.gameObject.SetActive(false);
        ActionItemManager.instance.IsActionItemCurrentlyVisible = false;
    }

    public void PickUpVR()
    {
        _VRButton.gameObject.SetActive(true);
        AmountOfVRs++;
    }
}
