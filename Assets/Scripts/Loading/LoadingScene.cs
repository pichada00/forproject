using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image barloading;
    public string sceneName;

    public void LoadScene(string sceneId)
    {
        if(sceneId == "Stage1Chapter1")
        {
            GameManager.Instance.coubtPassStage++;
        }
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync (string sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            barloading.fillAmount = progressValue;

            yield return null;
        }
        
    }
    public void continueScene()
    {
        if(GameManager.Instance.sceneInfostage0.stagepass == true)
        {
            StartCoroutine(LoadSceneAsync("Stage1Chapter1"));
        }
    }


}
