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

    [SerializeField] private Animator _hero;
    [SerializeField] private GameObject _fireStarter;

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
        _hero.Play("CharBadEndIdle");
        
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < _messages.Length; i++)
        {
            _selfTypingText.text = "";

            if (i == 2)
            {
                _hero.Play("CharBadEnd1");
                yield return new WaitForSeconds(1f);
            }
            
            yield return Utils.TypeText(_messages[i], _selfTypingText);

            if (i == 3)
            {
                _hero.Play("CharBadEnd2");
                yield return new WaitForSeconds(1f);
            }

            if (i == 6)
            {
                _hero.Play("CharBadEnd3");
                _fireStarter.SetActive(true);
            }

            if (i == 7)
            {
                StartCoroutine(WinningItemSpawner.Instance.SpawnFiresOnItems());
            }

            if (i == 8)
            {
                yield return new WaitForSeconds(5f);
            }
        }

        _finaleScreen.gameObject.SetActive(true);
        _finaleText.gameObject.SetActive(true);
    }
}
