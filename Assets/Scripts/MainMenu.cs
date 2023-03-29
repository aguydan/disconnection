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
        yield return new WaitForSeconds(1);

        for (int i = 0; i < _messages.Length; i++)
        {
            _textOutput.text = "";
            
            if (i == 5)
            {
                _beginScreenItems.SetActive(true);
            }

            yield return TypeText(_messages[i]);

            _beginScreenItems.SetActive(false);
        }

        yield return MenuGameManager.Instance.ContinueToNextScene("Game");
    }

    IEnumerator TypeText(string message)
    {
        char[] characterArray = message.ToCharArray();

        foreach (char letter in characterArray)
        {
            _textOutput.text += letter;
            yield return new WaitForSeconds(.08f);
        }

        yield return new WaitForSeconds(1f);
    }


}
