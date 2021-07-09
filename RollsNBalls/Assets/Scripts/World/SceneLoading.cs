using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public ProgressBar bar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        yield return new WaitForEndOfFrame();
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("Base Scene");
        while(gameLevel.progress < 1)
        {
            bar.setValue(gameLevel.progress);
            yield return new WaitForEndOfFrame();
        }
    }
}
