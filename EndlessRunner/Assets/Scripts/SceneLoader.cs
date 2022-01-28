using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    IEnumerator SwitchScene(int sceneNumber)
    {
        yield return new WaitForSeconds(2.8f);
        SceneManager.LoadScene(sceneNumber);
    }
    public void StartSwitchingScene(int sceneNumber)
    {
        StartCoroutine(SwitchScene(sceneNumber));
    }
}
