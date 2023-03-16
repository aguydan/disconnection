using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] _songs;
    [SerializeField] float _maxDistance = 8;
    public AudioSource MusicPlayer;
    
    Vector2 _winningItemPosition;

    void Start()
    {
        NextSongIndex();
        Debug.Log(ActionItemManager.instance.CurrentSong);
        _winningItemPosition = ItemSpawner.Instance.winningItem.transform.position;
        MusicPlayer.PlayOneShot(_songs[ActionItemManager.instance.CurrentSong]);
    }

    void Update()
    {
        float distanceToWinningItem = Vector2.Distance(_winningItemPosition, GameManager.instance.heroPosition);
        float finalScaler = 1f - Mathf.Clamp((distanceToWinningItem / _maxDistance), 0f, 0.8f);

        MusicPlayer.volume = finalScaler;
    }

    void NextSongIndex()
    {
        ActionItemManager.instance.CurrentSong++;

        if (ActionItemManager.instance.CurrentSong > _songs.Length)
        {
            ActionItemManager.instance.CurrentSong = 0;
        }
    }
}
