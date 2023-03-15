using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image _image;
    [HideInInspector] public Transform ParentBeforeDrag;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("drag begin");
        ParentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("drag end");
        transform.SetParent(ParentBeforeDrag);
        _image.raycastTarget = true;
    }
}
