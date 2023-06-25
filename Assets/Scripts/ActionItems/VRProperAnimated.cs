using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VRProperAnimated : MonoBehaviour
{
    [SerializeField] Animator _MYR;
    [SerializeField] GameObject _back;
    [SerializeField] GameObject _mask;
    [SerializeField] VRItem _VR;
    [SerializeField] GameObject _VRGuard;

    [SerializeField] private Volume _volume;
    [SerializeField] private VolumeProfile _VRProfile;

    public void ActivateVRFromAnimation()
    {
        ActionItemManager.instance.AIMActivateVR();
        StartCoroutine(ModelYourRoomLoading());
    }

    public void DeactivateVRProper()
    {
        gameObject.SetActive(false);
        _VRGuard.SetActive(false);
    }

    IEnumerator ModelYourRoomLoading()
    {
        _MYR.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        _volume.profile = _VRProfile;
        _MYR.gameObject.SetActive(false);
        _VR.DecreaseWrongItemsSortingOrder();
        _mask.SetActive(true);
        _back.SetActive(false);
        _VRGuard.SetActive(false);
    }
}
