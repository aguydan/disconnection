using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IDropHandler
{
    // [SerializeField] Image _sr;
    
    public bool IsOccupied = false;
    public bool IsRightCell = false;

    public void OnDrop(PointerEventData eventData)
    {
        if (!IsOccupied)
        {
            GameObject dropped = eventData.pointerDrag;
            Letter letter = dropped.GetComponent<Letter>();
            letter.ParentAfterDrag.GetComponent<Cell>().IsOccupied = false;
            letter.ParentAfterDrag = transform;
            IsOccupied = true;
        }
    }

    // private void Update() {
    //     if (IsOccupied)
    //     {
    //         _sr.color = Color.green;
    //     } else {
    //         _sr.color = Color.cyan;
    //     }
    // }
}
