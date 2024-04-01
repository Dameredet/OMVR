using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuseumLoadSystem : MonoBehaviour
{

    public IDataHandler JSONDataHandler = new JsonDataHandler();

    public void LoadScene(string sceneName, string currentSceneName, string FileToLoadPath = null)
    {
        PlayerPrefs.SetString("FileToLoad", FileToLoadPath);
        PlayerPrefs.Save();

        StartCoroutine(LoadSceneAsync(sceneName, currentSceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName, string currentSceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is null or empty.");
            yield break;
        }

        SceneManager.UnloadSceneAsync(currentSceneName);

        SceneManager.LoadScene(sceneName);

    }
}
