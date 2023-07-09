using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    string[] scenes;

    AsyncOperation async = null;

    static SceneChanger sceneManager = null;

    private void Awake()
    {
        if (sceneManager == null)
        {
            sceneManager = this;
        }
        else if(sceneManager != this)
        {
            Destroy(this);
        }
    }


    public static SceneChanger GetInstance()
    {
        return sceneManager;
    }

    private void Start()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        scenes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

    public void ChangeScene(string sceneName) {
        if(!SceneIsLoading()){
            for (int c = 0; c < scenes.Length; c++)
            {
                if (scenes[c] == sceneName)
                {
                    StartCoroutine(LoadScene(sceneName));
                }
            }
        }
    }

    public void ReloadScene()
    {
        if (!SceneIsLoading())
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
        }
    }

    public void Quit(){
        Application.Quit();
    }

    public bool SceneIsLoading()
    {
        if(async == null){
            return false;
        }
        return !async.isDone;
    }


    IEnumerator LoadScene (string scene)
    {
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            if(async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
