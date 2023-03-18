using UnityEngine;

public class VolumeBasedOnDistance : MonoBehaviour
{
    [SerializeField] float _maxDistance = 8;
    
    Vector2 _winningItemPosition;
    public float VolumeScaler;

    void Start()
    {
        _winningItemPosition = ItemSpawner.Instance.winningItem.transform.position;
    }

    void Update()
    {
        VolumeScaler = CalculateVolumeBasedOnDistance();
    }

    float CalculateVolumeBasedOnDistance()
    {
        float distanceToWinningItem = Vector2.Distance(_winningItemPosition, GameManager.instance.heroPosition);
        
        return 1f - Mathf.Clamp((distanceToWinningItem / _maxDistance), 0.1f, 1f);
    }
}
