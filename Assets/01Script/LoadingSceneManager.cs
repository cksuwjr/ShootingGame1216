using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;
    private AsyncOperation asyncScene;
    private float timeC;

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetString(SAVE_Type.SAVE_SceneName.ToString()) + "씬을 로드할거야");

        loadingBar.fillAmount = 0f;
        StartCoroutine("LoadSceneAsync");
    }

    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(3f);
        asyncScene = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(SAVE_Type.SAVE_SceneName.ToString()));
        asyncScene.allowSceneActivation = false;
        timeC = 0.0f;

        while (!asyncScene.isDone)
        {
            timeC += Time.deltaTime;

            if(asyncScene.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);
                if (loadingBar.fillAmount > 0.99f)
                    asyncScene.allowSceneActivation = true;
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if (loadingBar.fillAmount >= asyncScene.progress)
                    timeC = 0.0f;
            }
            yield return null;
        }
    }
}
