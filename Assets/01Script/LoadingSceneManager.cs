using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetString(SAVE_Type.SAVE_SceneName.ToString()) + "씬을 로드할거야");
    }
}
