using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    IEnumerator SwitchScene(int sceneNumber)
    {
        UIManager.instance.Fade(true);
        yield return new WaitForSecondsRealtime(2.8f);
        SceneManager.LoadScene(sceneNumber);
    }
    public void StartSwitchingScene(int sceneNumber)
    {
        StartCoroutine(SwitchScene(sceneNumber));
    }

    public static SceneLoader instance;

    private void Awake()
    {
        instance = this;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
