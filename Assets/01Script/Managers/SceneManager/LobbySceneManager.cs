using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;


public enum MenuType
{
    Menu_Enchant = 1,
    Menu_Option = 5,
}



public class LobbySceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerLevelText;       
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Image expBar;

    private void Awake()
    {
        InitLobbyScene();
    }

    private void InitLobbyScene()
    {
        playerLevelText.text = PlayerPrefs.GetInt(SAVE_Type.SAVE_Level.ToString()).ToString();
        goldText.text = PlayerPrefs.GetInt(SAVE_Type.SAVE_GOLD.ToString()).ToString();
        expBar.fillAmount = PlayerPrefs.GetInt(SAVE_Type.SAVE_EXP.ToString()) / 300.0f;
    }

    // 게임 시작 버튼

    public void GameStart()
    {
        PlayerPrefs.SetString(SAVE_Type.SAVE_SceneName.ToString(), SCENE_Name.BattleScene.ToString());
        SceneManager.LoadScene(SCENE_Name.LoadingScene.ToString());
    }
}
