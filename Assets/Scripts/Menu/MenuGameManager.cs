using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameManager : MonoBehaviour
{
    public static MenuGameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public IEnumerator ContinueToNextScene(string sceneName)
    {
        Transition.Instance.CloseTransition();
            
        yield return new WaitForSeconds(1);
            
        SceneManager.LoadScene(sceneName);
    }
}
