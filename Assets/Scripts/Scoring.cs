using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public static int EscapismScore = 20;
    public static int MoodScore = 25;

    public static List<Sprite> WinningItemSprites = new List<Sprite>();

    public static int MaxItemAmount = 50;
    public static int FurnSetIndex = -1;
    
    public static int AIMPSongIndex = 0;

    public struct EntryItem
    {
        public string Name;
        public Sprite Sprite;
        public Sprite GrayscaleSprite;
        public bool IsGathered;
    };
    
    public struct CategoryInfo
    {
        public bool IsCompleted;
        public bool IsCompletionRewardUsed;
        public List<EntryItem> CategItems;
    };

    public static Dictionary<ItemSprite.ItemCategory, CategoryInfo> NotepadCategories = new Dictionary<ItemSprite.ItemCategory, CategoryInfo>();
}
