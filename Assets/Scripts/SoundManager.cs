using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource _musicSource, _effectsSource, _footstepsSource;
    public AudioClip[] Effects;
    public AudioClip[] Music;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFootsteps(AudioClip clip)
    {
        if (!_footstepsSource.isPlaying)
        {
            _footstepsSource.PlayOneShot(clip);
        }
    }

    public void PlayEffect(AudioClip clip, float volume = 1)
    {
        if (!_effectsSource.isPlaying)
        {
            _effectsSource.PlayOneShot(clip, volume);
        }
    }

    public void PlayMusic(AudioClip clip, float volume = 1)
    {
        if (!_musicSource.isPlaying)
        {
            _musicSource.PlayOneShot(clip, volume);
        }
    }

    public void PlayEffectUnopposed(AudioClip clip, float volume = 1)
    {
        _effectsSource.PlayOneShot(clip, volume);
    }

    public void StopEffects()
    {
        _effectsSource.Stop();
    }

    public void StopFootsteps()
    {
        _footstepsSource.Stop();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}
