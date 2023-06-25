using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _MPProper;
    [SerializeField] private RectTransform _soundwaves;
    
    [SerializeField] private GameObject _guard;

    public void Update()
    {
        _soundwaves.localScale = new Vector3(0.9f, AIMPManager.Instance.Volume * 1.2f, 0);
        _soundwaves.transform.position = new Vector3(207.34f, 215.36f + AIMPManager.Instance.Volume * 32, 0);
    }
    
    public void LaunchStartingAnimation()
    {
        _guard.SetActive(true);
    }

    public void LaunchStartingTrigger()
    {
        ActionItemManager.instance.ActivateMusicPlayer();
        _soundwaves.gameObject.SetActive(true);
        _guard.SetActive(false);
    }

    public void LaunchExitAnimation()
    {
        ActionItemManager.instance.DeactivateMusicPlayer();
        _MPProper.Play("exitMusicPlayer");
        _soundwaves.gameObject.SetActive(false);
        _guard.SetActive(true);
    }

    public void LaunchExitTrigger()
    {
        _guard.SetActive(false);
        gameObject.SetActive(false);
    }
}
