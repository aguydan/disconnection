using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMPManager : MonoBehaviour
{
    [SerializeField] AudioSource _AIMPSource;
    [SerializeField] AudioClip[] _AIMPSongs;
    [SerializeField] VolumeBasedOnDistance _volumeCalculator;

    private void Update()
    {
        _AIMPSource.volume = _volumeCalculator.VolumeScaler;
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
}
