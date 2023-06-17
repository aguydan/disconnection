using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public static int EscapismScore = 20;
    public static int MoodScore = 7;

    public static int GetCurrentMoodStage()
    {
        int stage = 3;
        
        if (MoodScore <= 0)
        {
            stage = 0;
        }
        else if (MoodScore > 0 && MoodScore <= 3)
        {
            stage = 1;
        }
        else if (MoodScore > 3 && MoodScore <= 6)
        {
            stage = 2;
        }
        else if (MoodScore > 9 && MoodScore <= 12)
        {
            stage = 4;
        }
        else if (MoodScore > 12)
        {
            stage = 5;
        }

        return stage;
    }

    public static List<Sprite> WinningItemSprites = new List<Sprite>();

    public static int MaxSocialMediaItemsAmount = 2;
    public static bool AreSMLocationsChosen = false;
    public static int SMLocation1 = 0;
    public static int SMLocation2 = 0;

    public static int TelescopeRoomIndex = 0;
    public static bool IsTelescopeIndexSet = false;

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
