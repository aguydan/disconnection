using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableFurniture : MonoBehaviour, IPointerDownHandler
{
    public BoxCollider2D BoxCollider;
    
    public bool HasHint = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (HasHint)
        {
            Debug.Log("is winning");
        }
        else 
        {
            Debug.Log("clicked");
        }
    }
}
