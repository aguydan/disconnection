using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialMediaAnimator : MonoBehaviour
{
    [SerializeField] private Animator _SMProper;
   
    [SerializeField] private ScrollRect _scrollRect;
    private Vector2 _currentValue;

    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _guard;

    private void OnEnable()
    {
        _scrollRect.onValueChanged.AddListener(DetermineScrollingAnimation);
    }

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
        _SMProper.speed = 1.5f;
        _SMProper.Play("closeSocialMedia");
        _guard.SetActive(true);
    }

    public void LaunchExitTrigger()
    {
        _guard.SetActive(false);
        _background.SetActive(false);
        gameObject.SetActive(false);
    }

    private void DetermineScrollingAnimation(Vector2 value)
    {
        _SMProper.Play("swipeSocialMedia");
        _SMProper.speed = 1f;

        _currentValue = value;
        StartCoroutine(IsScrollingStopped());

        IEnumerator IsScrollingStopped()
        {
            yield return new WaitForSeconds(0.4f);

            if (_currentValue == value) _SMProper.speed = 0;
        }
    }

    private void OnDisable()
    {
        _scrollRect.onValueChanged.RemoveListener(DetermineScrollingAnimation);
    }
}
