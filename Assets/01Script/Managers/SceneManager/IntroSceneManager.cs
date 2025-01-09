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

    // ���� �����Ͱ� �ִ��� �˻�, ������ ȯ��, ������ ������������
    private void InitIntroScene()
    {
        // ĳ�������, �����ȯ�� ������ �ʱ�ȭ��
        // PlayerPrefs << �ӽ� ������ ����

        // �г����� ���ڼ��� �α��� �̸��̸� �����Ͱ� ���ٴ� ���
        if (PlayerPrefs.GetString(SAVE_Type.SAVE_NickName.ToString()).Length < 2)
        {
            welcomeText.gameObject.SetActive(false); // ȯ���޽��� ������ �ʱ�
            inputField.gameObject.SetActive(true);
            haveUserInfo = false;
        }
        else
        {
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = PlayerPrefs.GetString(SAVE_Type.SAVE_NickName.ToString()) + "�� ȯ���մϴ�.";
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
                Debug.Log("���� ����: " + inputField.text);
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
