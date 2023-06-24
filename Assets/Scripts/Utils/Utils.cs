using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Utils
{
    public static IEnumerator TypeText(string message, TextMeshProUGUI textOutput)
    {
        char[] characterArray = message.ToCharArray();
        SoundManager.Instance.PlayEffectUnopposed(SoundManager.Instance.Effects[11]);

        foreach (char letter in characterArray)
        {
            textOutput.text += letter;
            yield return new WaitForSeconds(.08f);
        }

        SoundManager.Instance.StopEffects();

        yield return new WaitForSeconds(1f);
    }
}
