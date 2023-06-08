using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotepadEntry : MonoBehaviour
{
    [SerializeField] private EntryTop _entryTop;
    
    public struct EntryItem {
        public string Name;
        public Sprite Sprite;
        public Sprite GrayscaleSprite;
        public EntryGridCell Cell;
    };

    public List<EntryItem> EntryItems = new List<EntryItem>();

    public void UpdateEntry(Item item)
    {
        Debug.Log("weere here!!");
        foreach (EntryItem entryItem in EntryItems)
        {
            if (entryItem.Name == item.look.sprite.name)
            {
                entryItem.Cell.CellImage.sprite = entryItem.Sprite;
                EntryItems.Remove(entryItem);
                break;
            }
            else
            {
                Debug.Log("bad");
            }
        }
    }
}
