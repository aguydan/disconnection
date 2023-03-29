using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableFurniture : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Animator _animator;
    
    public Collider2D Collider;
    public bool HasHint = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (HasHint)
        {
            UIManager.instance.CallActionItemPopup();
            SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[9]);
        }
        else
        {
            SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[7]);
        }

        int randomIndex = Random.Range(0, FurnitureSpawner.Instance.InteractableFurniturePrefabs.Length);

        while (_renderer.sprite == FurnitureSpawner.Instance.InteractableFurniturePrefabs[randomIndex]._renderer.sprite)
        {
            randomIndex = Random.Range(0, FurnitureSpawner.Instance.InteractableFurniturePrefabs.Length);
        }

        _renderer.sprite = FurnitureSpawner.Instance.InteractableFurniturePrefabs[randomIndex]._renderer.sprite;
        _animator.Play("InteractableFurnitureClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = true;
        
        CursorManager.Instance.EnableFingerCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.StopAutomaticCursor = false;
    }
}
