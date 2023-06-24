using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaAnimator : MonoBehaviour
{
   [SerializeField] private Animator _SMProper;
   
   [SerializeField] private GameObject _background;
   [SerializeField] private GameObject _guard;

   public void LaunchStartingAnimation()
    {
        _guard.SetActive(true);
        _background.SetActive(true);
    }

    public void LaunchStartingTrigger()
    {
        ActionItemManager.instance.SMMActivateSocialMedia();
        _guard.SetActive(false);
    }

    public void LaunchExitAnimation()
    {
        ActionItemManager.instance.SMMDeactivateSocialMedia();
        _SMProper.Play("closeSocialMedia");
        _guard.SetActive(true);
    }

    public void LaunchExitTrigger()
    {
        _guard.SetActive(false);
        _background.SetActive(false);
        gameObject.SetActive(false);
    }
}
