using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManager : MonoBehaviour
{
    [SerializeField] private VRItem _VR;

    public int AmountOfVRs = 0;

    public void ActivateVR()
    {
        _VR.gameObject.SetActive(true);
        _VR.ClearCrosses();
        _VR.IdentifyWrongItems();
        StartCoroutine(_VR.SpawnCrosses());
    }
}
