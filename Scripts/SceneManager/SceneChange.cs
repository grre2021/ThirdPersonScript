using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : Singleton<SceneChange>
{
    private string nextSceneName;

    public void LoadNextLevel(string sceneName)
    {
        nextSceneName = sceneName;
        StartCoroutine(nameof(LoadLevel));
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName);
        operation.allowSceneActivation=false;
        while (!operation.isDone)
        {
            MeunController.Instance.Load(operation.progress);
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}