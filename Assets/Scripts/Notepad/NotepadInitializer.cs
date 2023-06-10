using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadInitializer : MonoBehaviour
{
    public static NotepadInitializer Instance;
    
    [SerializeField] private bool _isNotepadInitialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!_isNotepadInitialized)
        {
            InitializeNotepadCategories();
            _isNotepadInitialized = true;
        }
    }

    public void InitializeNotepadCategories()
    {
        //создание категории для сохранения в скоринг
        string[] categNames = Enum.GetNames(typeof(ItemSprite.ItemCategory));

        foreach (string name in categNames)
        {
            ItemSprite.ItemCategory category = (ItemSprite.ItemCategory)Enum.Parse(typeof(ItemSprite.ItemCategory), name);
            
            Scoring.NotepadCategories.Add(category, new Scoring.CategoryInfo {
                IsCompleted = false,
                IsCompletionRewardUsed = false,
                CategItems = new List<Scoring.EntryItem>()
            });
        }
        
        //распределение предметов по категориям
        foreach (ItemSprite item in ItemSpawner.Instance.ItemSprites)
        {
            Scoring.NotepadCategories[item.Category].CategItems.Add(new Scoring.EntryItem{
                Name = item.Sprite.name,
                Sprite = item.Sprite,
                GrayscaleSprite = item.GrayscaleSprite,
                IsGathered = false
            });
        }
    }
}
