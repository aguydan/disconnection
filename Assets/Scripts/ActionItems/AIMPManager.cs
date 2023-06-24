using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMPManager : MonoBehaviour
{
    [SerializeField] AudioSource _AIMPSource;
    [SerializeField] AudioClip[] _AIMPSongs;
    [SerializeField] VolumeBasedOnDistance _volumeCalculator;

    public MusicPlayerAnimator Animator;
    public float Volume;

    public static AIMPManager Instance;
    
    private void Awake() => Instance = this;
    
    private void Update()
    {
        _AIMPSource.volume = _volumeCalculator.VolumeScaler;
        Volume = _AIMPSource.volume;
    }
    
    public void PlayVolumeControlSong()
    {
        _volumeCalculator.gameObject.SetActive(true);

        _AIMPSource.PlayOneShot(_AIMPSongs[Scoring.AIMPSongIndex]);
    }

    public void StopVolumeControlSong()
    {
        _AIMPSource.Stop();
        _AIMPSource.volume = 0;

        _volumeCalculator.gameObject.SetActive(false);
    }

    public void NextSongIndex()
    {
        Scoring.AIMPSongIndex++;

        if (Scoring.AIMPSongIndex == _AIMPSongs.Length)
        {
            Scoring.AIMPSongIndex = 0;
        }
    }

    public void ActivateMusicPlayerProper()
    {
        Animator.gameObject.SetActive(true);
        Animator.LaunchStartingAnimation();
    }
}
