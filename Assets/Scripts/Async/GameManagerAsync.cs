using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerAsync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this);
       StartCoroutine(  LoadSceneAsync());

    }

     IEnumerator LoadSceneAsync()
    {
        Debug.Log("Preload "+ Time.time);
        AsyncOperation asyncOperation=   SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        
        asyncOperation.allowSceneActivation = false;
        asyncOperation.completed += AsyncOperation_completed;
        yield return new WaitForEndOfFrame();
        while(asyncOperation.progress<0.9f)
        {
            Debug.Log("cargando "+ asyncOperation.progress + Time.time);
            yield return null;
            Debug.Log("after wield");
        }
        Debug.Log("after while" );
        asyncOperation.allowSceneActivation = true;
        // asyncOperation.progress
        // SceneManager.LoadScene
        
    }

    private void AsyncOperation_completed(AsyncOperation obj)
    {
        Debug.Log("escena cargada"+Time.time);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));

        AsyncOperation unloadAsyncOp = SceneManager.UnloadSceneAsync(0, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        unloadAsyncOp.completed += UnloadAsyncOp_completed;

    }

    private void UnloadAsyncOp_completed(AsyncOperation obj)
    {
        Debug.Log("escena loader descargada" + Time.time);
    }
}
