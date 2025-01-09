using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum SAVE_Type
{
    SAVE_NickName,
    SAVE_SceneName,
    SAVE_SFX,
    SAVE_BGM,
    SAVE_Level,
    SAVE_EXP,
    SAVE_GOLD,

}

public enum SCENE_Name
{
    IntroScene,
    LoadingScene,
    LobbyScene,
    BattleScene,
}

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI welcomeText;

    private bool haveUserInfo = false;

    private void Awake()
    {
        InitIntroScene();
    }

    // 유저 데이터가 있는지 검사, 있으면 환영, 없으면 계정생성로직
    private void InitIntroScene()
    {
        // 캐시지우기, 기계전환시 데이터 초기화됨
        // PlayerPrefs << 임시 데이터 저장

        // 닉네임의 글자수가 두글자 미만이면 데이터가 없다는 취급
        if (PlayerPrefs.GetString(SAVE_Type.SAVE_NickName.ToString()).Length < 2)
        {
            welcomeText.gameObject.SetActive(false); // 환영메시지 보이지 않기
            inputField.gameObject.SetActive(true);
            haveUserInfo = false;
        }
        else
        {
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = PlayerPrefs.GetString(SAVE_Type.SAVE_NickName.ToString()) + "님 환영합니다.";
            inputField.gameObject.SetActive(false);
            haveUserInfo = true;
        }
    }

    public void GameStartBtn()
    {
        if(!haveUserInfo)
        {
            if(inputField.text.Length >= 2)
            {
                Debug.Log("계정 생성: " + inputField.text);
                CreateUserData(inputField.text);
                haveUserInfo = true;
            }
        }

        if (haveUserInfo)
        {
            PlayerPrefs.SetString(SAVE_Type.SAVE_SceneName.ToString(), SCENE_Name.LobbyScene.ToString());
            SceneManager.LoadScene(SCENE_Name.LoadingScene.ToString());
        }



    }

    private void CreateUserData(string userNickName)
    {
        PlayerPrefs.SetString(SAVE_Type.SAVE_NickName.ToString(), userNickName);
        PlayerPrefs.SetInt(SAVE_Type.SAVE_Level.ToString(), 1);
        PlayerPrefs.SetInt(SAVE_Type.SAVE_EXP.ToString(), 0);
        PlayerPrefs.SetFloat(SAVE_Type.SAVE_SFX.ToString(), 1.0f);
        PlayerPrefs.SetFloat(SAVE_Type.SAVE_BGM.ToString(), 1.0f);

    }
}
