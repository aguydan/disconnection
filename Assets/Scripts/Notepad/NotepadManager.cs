using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NotepadManager : MonoBehaviour
{
    public static NotepadManager Instance;

    [SerializeField] private GameObject _notepad;
    [SerializeField] private Button _notepadButton;

    [SerializeField] private VerticalLayoutGroup _entryLayout;
    [SerializeField] private NotepadEntry _entryPrefab;
    [SerializeField] private EntryTop _entryTopPrefab;
    [SerializeField] private EntryGridCell _entryGridCellPrefab;

    [SerializeField] private GameObject _background;
    
    ///этот должен быть в soring?
    private Dictionary<ItemSprite.ItemCategory, NotepadEntry> _spawnedEntries = new Dictionary<ItemSprite.ItemCategory, NotepadEntry>();
    private Dictionary<ItemSprite.ItemCategory, string> _categDisplayNames = new Dictionary<ItemSprite.ItemCategory, string>{
        {ItemSprite.ItemCategory.Instrument, "Инструменты"},
        {ItemSprite.ItemCategory.Toy, "Игрушки"},
    };
    
    private void Awake()
    {
        Instance = this;

        //dont destroy on load!!!!
    }

    private void Start()
    {
        SpawnNotepadEntries();
        StartCoroutine(RefreshLayout());
    }

    private void SpawnNotepadEntries()
    {
        string[] names = Enum.GetNames(typeof(ItemSprite.ItemCategory));

        //спаун энтри
        foreach (string name in names)
        {
            ItemSprite.ItemCategory category = (ItemSprite.ItemCategory)Enum.Parse(typeof(ItemSprite.ItemCategory), name);
            
            EntryTop entryTop = Instantiate(_entryTopPrefab);
            entryTop.transform.SetParent(_entryLayout.transform, false);
            entryTop.SetLabel(_categDisplayNames[category]);
            
            NotepadEntry entry = Instantiate(_entryPrefab);
            entry.transform.SetParent(_entryLayout.transform, false);

            _spawnedEntries.Add(category, entry);
        }

        //спаун предметов в энтри
        foreach (ItemSprite item in ItemSpawner.Instance.ItemSprites)
        {
            NotepadEntry entry = _spawnedEntries[item.Category];

            EntryGridCell cell = Instantiate(_entryGridCellPrefab);
            cell.transform.SetParent(entry.transform, false);
            
            cell.CellImage.sprite = item.GrayscaleSprite;
            cell.CellImage.SetNativeSize();
            cell.CellImage.preserveAspect = true;
            cell.CellImage.rectTransform.sizeDelta = new Vector2(cell.CellImage.rectTransform.sizeDelta.x, 12);

            entry.EntryItems.Add(new NotepadEntry.EntryItem {
                Name = item.Sprite.name,
                Sprite = item.Sprite,
                GrayscaleSprite = item.GrayscaleSprite,
                Cell = cell
            });
        }
    }

    public void UpdateNotepad(Item item)
    {
        NotepadEntry currentEntry = _spawnedEntries[item.Category];
        currentEntry.UpdateEntry(item);
    }

    public void ActivateNotepad()
    {
        _notepad.SetActive(true);
        _background.SetActive(true);
    }

    public void DeactivateNotepad()
    {
        _notepad.SetActive(false);
        _background.SetActive(false);
    }

    private IEnumerator RefreshLayout()
    {
        _notepad.SetActive(true);
        _entryLayout.GetComponent<VerticalLayoutGroup>().enabled = false;
        yield return new WaitForSeconds(0.001f);
        _entryLayout.GetComponent<VerticalLayoutGroup>().enabled = true;
        _notepad.SetActive(false);
    }
}
