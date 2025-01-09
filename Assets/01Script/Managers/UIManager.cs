using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Transform canvasTransform; // 총괄 canvas Trans
    private TextMeshProUGUI scoreText; // 점수 텍스트
    private TextMeshProUGUI jamText; // 보석 텍스트
    private TextMeshProUGUI boomText; // 폭탄 개수 텍스트

    [SerializeField] private Image[] heartsImg;


    private GameObject obj;
    private Transform tr;

    private TextMeshProUGUI ScoreText
    {
        get
        {
            if(scoreText == null)
            {
                tr = MyUtility.FindChildRescursive(canvasTransform, "ScoreText");
                if(tr != null )
                {
                    scoreText = tr.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.Log("UIManager.cs - ScoreText - 참조실패");
                    return null;
                }
            }
            return scoreText;
        }
    }

    private TextMeshProUGUI JamText
    {
        get
        {
            if (jamText == null)
            {
                tr = MyUtility.FindChildRescursive(canvasTransform, "JamText");
                if (tr != null)
                {
                    jamText = tr.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.Log("UIManager.cs - jamText - 참조실패");
                    return null;
                }
            }
            return jamText;
        }
    }

    private TextMeshProUGUI BoomText
    {
        get
        {
            if (boomText == null)
            {
                tr = MyUtility.FindChildRescursive(canvasTransform, "BoomText");
                if (tr != null)
                {
                    boomText = tr.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.Log("UIManager.cs - BoomText - 참조실패");
                    return null;
                }
            }
            return boomText;
        }
    }


    private void Awake()
    {
        obj = GameObject.Find("Canvas");
        canvasTransform = obj.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        ScoreManager.OnChangeScore += UpdateScoreText;
        ScoreManager.OnChangeBomb += UpdateBombText;
        ScoreManager.OnChangeJamCount += UpdateJamText;
        ScoreManager.OnChangeHp += UpdatePlayerHp;
    }

    private void OnDisable()
    {
        ScoreManager.OnChangeScore -= UpdateScoreText;
        ScoreManager.OnChangeBomb -= UpdateBombText;
        ScoreManager.OnChangeJamCount -= UpdateJamText;
        ScoreManager.OnChangeHp -= UpdatePlayerHp;
    }

    private void UpdatePlayerHp(int score)
    {
        for(int i = 0; i < 5; i++)
        {
            if(i < score)
            {
                heartsImg[i].enabled = true;
            }
            else
            {
                heartsImg[i].enabled = false;
            }
        }
    }

    private void UpdateJamText(int score)
    {
        JamText.text = score.ToString();
    }

    private void UpdateBombText(int score)
    {
        BoomText.text = "X : " + score.ToString();
    }

    private void UpdateScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }
}
