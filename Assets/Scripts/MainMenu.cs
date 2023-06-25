using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image _beginScreen;
    [SerializeField] GameObject _beginScreenItems;
    [SerializeField] GameObject _scoreItems;
    [SerializeField] TextMeshProUGUI _textOutput;
    [SerializeField] string[] _messages;

    private Coroutine _currentCoroutine;
    
    public void StartGame()
    {
        _currentCoroutine = StartCoroutine(BeginScreen());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator BeginScreen()
    {
        Transition.Instance.CloseTransition();
        yield return new WaitForSeconds(1);

        _beginScreen.gameObject.SetActive(true);
        Transition.Instance.OpenTransition();

        SoundManager.Instance.PlayMusic(SoundManager.Instance.Music[0]);
        yield return new WaitForSeconds(1);

        for (int i = 0; i < _messages.Length; i++)
        {
            _textOutput.text = "";
            
            if (i == 5)
            {
                SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[9]);
                _beginScreenItems.SetActive(true);
            }

            if (i == 6)
            {
                _beginScreenItems.SetActive(false);
                // SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[9]);
                _scoreItems.SetActive(true);
            }

            if (i == 9)
            {
                _scoreItems.SetActive(false);
            }


            yield return Utils.TypeText(_messages[i], _textOutput);
        }

        SoundManager.Instance.StopMusic();
        yield return MenuGameManager.Instance.ContinueToNextScene("Game");
    }

    public void SkipBeginScreen()
    {
        StopCoroutine(_currentCoroutine);
        SoundManager.Instance.StopMusic();
        StartCoroutine(MenuGameManager.Instance.ContinueToNextScene("Game"));
    }
}
