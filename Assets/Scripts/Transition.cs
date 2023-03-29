using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] Animator _animator;
    
    public static Transition Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    public void CloseTransition()
    {
        SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[3]);
        _animator.Play("Transition");
    }

    public void OpenTransition()
    {
        SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[3]);
        _animator.Play("TransitionOpen");
    }
}
