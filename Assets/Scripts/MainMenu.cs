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
    [SerializeField] TextMeshProUGUI _textOutput;
    [SerializeField] string[] _messages;
    
    public void StartGame()
    {
        StartCoroutine(BeginScreen());
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

            yield return Utils.TypeText(_messages[i], _textOutput);
            _beginScreenItems.SetActive(false);
        }

        SoundManager.Instance.StopMusic();
        yield return MenuGameManager.Instance.ContinueToNextScene("Game");
    }
}
