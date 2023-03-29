using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinaleGameManager : MonoBehaviour
{
    [SerializeField] Image _finaleScreen;
    [SerializeField] TextMeshProUGUI _finaleText;

    public static FinaleGameManager Instance;
    public bool StopPlayerInput = false;

    private void Awake() {
        Instance = this;
    }

    public IEnumerator GameFinale()
    {
        yield return new WaitForSeconds(1.5f);
        _finaleScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        _finaleText.gameObject.SetActive(true);
    }
}
