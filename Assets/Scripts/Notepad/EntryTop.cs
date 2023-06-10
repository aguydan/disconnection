using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryTop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    
    [SerializeField] private Sprite[] _moodButtonSprites;
    [SerializeField] private Button _moodButton;

    public ItemSprite.ItemCategory Category;

    public void SetLabel(string label)
    {
        _label.text = label;
    }

    public void ChangeButtonInteractivity(bool isInteractable)
    {
        _moodButton.interactable = isInteractable;
    }

    public void PlayMoodCutsceneWithCategory() {
        NotepadManager.Instance.PlayMoodCutscene(Category);
        _moodButton.interactable = false;
    }
}
