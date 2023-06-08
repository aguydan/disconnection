using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryTop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    
    [SerializeField] private Sprite[] _moodButtonSprites;
    [SerializeField] private Image _moodButton;

    public void SetLabel(string label)
    {
        _label.text = label;
    }
}
