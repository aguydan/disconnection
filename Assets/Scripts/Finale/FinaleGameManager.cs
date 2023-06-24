using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FinaleGameManager : MonoBehaviour
{
    [SerializeField] Image _finaleScreen;
    [SerializeField] TextMeshProUGUI _finaleText;

    [SerializeField] TextMeshProUGUI _selfTypingText;
    [SerializeField] string[] _messages;

    public static FinaleGameManager Instance;
    public bool StopPlayerInput = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BadEnding") StartCoroutine(BadEndingSequence());
    }

    public IEnumerator GameFinale()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.Music[1], 0.2f);
        
        yield return new WaitForSeconds(1.5f);
        _finaleScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        _finaleText.gameObject.SetActive(true);
    }

    private IEnumerator BadEndingSequence()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < _messages.Length; i++)
        {
            _selfTypingText.text = "";

            if (i == 2) WinningItemSpawner.Instance.SpawnFiresOnItems();

            yield return Utils.TypeText(_messages[i], _selfTypingText);
        }

        _finaleScreen.gameObject.SetActive(true);
        _finaleText.gameObject.SetActive(true);
    }
}
