using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerItem : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] float _maxDistance = 8;

    Vector2 _winningItemPosition;

    private void Start()
    {
        _winningItemPosition = ItemSpawner.Instance.winningItem.transform.position;
    }

    private void Update()
    {
        float distanceToWinningItem = Vector2.Distance(_winningItemPosition, GameManager.instance.heroPosition);

        float finalScaler = 1f - Mathf.Clamp((distanceToWinningItem / _maxDistance), 0f, 0.8f);

        _audioSource.volume = finalScaler;
    }
}
