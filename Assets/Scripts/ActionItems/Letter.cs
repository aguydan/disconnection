using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image _image;
    [HideInInspector] public Transform ParentAfterDrag;

    public char Name;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!BookItem.Instance.IsBookCompleted)
        {
            ParentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            _image.raycastTarget = false;
        }
        else
        {
            ParentAfterDrag = transform.parent;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!BookItem.Instance.IsBookCompleted)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!BookItem.Instance.IsBookCompleted)
        {
            transform.SetParent(ParentAfterDrag);
            _image.raycastTarget = true;
        
            if (ParentAfterDrag.GetComponent<Cell>().IsRightCell) BookItem.Instance.UpdateFace();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CursorManager.Instance.EnableCantGrabCursor();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CursorManager.Instance.EnableCanGrabCursor();
    }
}
