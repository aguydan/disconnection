using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


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
    [SerializeField] private Animator _cutsceneScreen;

    public bool IsNotepadVisible = false;
    
    private Dictionary<ItemSprite.ItemCategory, NotepadEntry> _spawnedEntries = new Dictionary<ItemSprite.ItemCategory, NotepadEntry>();
    private Dictionary<ItemSprite.ItemCategory, string> _categDisplayNames = new Dictionary<ItemSprite.ItemCategory, string>{
        {ItemSprite.ItemCategory.Instrument, "Инструменты"},
        {ItemSprite.ItemCategory.Toy, "Игрушки"},
        {ItemSprite.ItemCategory.Book, "Книги"},
    };
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnNotepadEntries();
        StartCoroutine(RefreshLayout());
    }

    private void SpawnNotepadEntries()
    {
        //спаун энтри
        foreach (KeyValuePair<ItemSprite.ItemCategory, Scoring.CategoryInfo> pair in Scoring.NotepadCategories)
        {
            EntryTop entryTop = Instantiate(_entryTopPrefab);
            entryTop.transform.SetParent(_entryLayout.transform, false);
            entryTop.SetLabel(_categDisplayNames[pair.Key]);
            entryTop.Category = pair.Key;
            
            NotepadEntry entry = Instantiate(_entryPrefab);
            entry.transform.SetParent(_entryLayout.transform, false);

            entry.EntryTop = entryTop;
            if (pair.Value.IsCompleted && !pair.Value.IsCompletionRewardUsed)
            {
                entry.EntryTop.ChangeButtonInteractivity(true);
            }
            else
            {
                entry.EntryTop.ChangeButtonInteractivity(false);
            }

            foreach (Scoring.EntryItem item in pair.Value.CategItems)
            {
                EntryGridCell cell = Instantiate(_entryGridCellPrefab);
                cell.transform.SetParent(entry.transform, false);
            
                cell.CellImage.sprite = item.IsGathered ? item.Sprite : item.GrayscaleSprite;
                cell.CellImage.SetNativeSize();
                cell.CellImage.preserveAspect = true;
                cell.CellImage.rectTransform.sizeDelta = new Vector2(cell.CellImage.rectTransform.sizeDelta.x, 12);

                entry.Cells.Add(cell);
            }

            _spawnedEntries.Add(pair.Key, entry);
        }
    }

    public void UpdateNotepad(Item item)
    {
        //обновляет спрайт на цветной если предмет попадает в блокнот, а также записывает изменения в скоринге
        //если категория завершена, возвращаемся из функции
        Scoring.CategoryInfo info = Scoring.NotepadCategories[item.Category];
        if (info.IsCompleted) return;
        
        List<Scoring.EntryItem> itemList = info.CategItems;
        
        //если предмет уже был подобран, выбираемся из функции
        if (itemList.Any(entryItem => entryItem.Name == item.OriginalSprite.name && entryItem.IsGathered)) return;
        
        Sprite updatedSprite;
        NotepadEntry entry = _spawnedEntries[item.Category];

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].Name == item.OriginalSprite.name)
            {
                var v = itemList[i];
                v.IsGathered = true;
                updatedSprite = v.Sprite;
                itemList[i] = v;

                foreach (EntryGridCell cell in entry.Cells)
                {
                    if (cell.CellImage.sprite.name == item.OriginalSprite.name)
                    {
                        cell.CellImage.sprite = updatedSprite;
                        break;
                    }
                }

                break;
            }
        }

        info.CategItems = itemList;

        //проверка, завершена ли категория
        if (itemList.All(entryItem => entryItem.IsGathered))
        {
            info.IsCompleted = true;
            //всё что касается завершения
            entry.EntryTop.ChangeButtonInteractivity(true);
        }

        Scoring.NotepadCategories[item.Category] = info;
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
        IsNotepadVisible = false;
        CursorManager.Instance.StopAutomaticCursor = false;
    }

    public void PlayMoodCutscene(ItemSprite.ItemCategory cat)
    {
        Scoring.CategoryInfo info = Scoring.NotepadCategories[cat];
        info.IsCompletionRewardUsed = true;
        Scoring.NotepadCategories[cat] = info;
        
        ScoreManager.instance.IncreaseMood(6);
        DeactivateNotepad();
        _cutsceneScreen.gameObject.SetActive(true);
        //играть катсцену с анимацией (наверно просто преобразуем категорию в стринг названия анимации
        StartCoroutine(TestCoroutine());

    }

    private IEnumerator RefreshLayout()
    {
        _notepad.SetActive(true);
        _entryLayout.GetComponent<VerticalLayoutGroup>().enabled = false;
        yield return new WaitForSeconds(0.001f);
        _entryLayout.GetComponent<VerticalLayoutGroup>().enabled = true;
        _notepad.SetActive(false);
    }

    private IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _cutsceneScreen.gameObject.SetActive(false);
    }
}
