using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // C# �����ϴ� ����
    public delegate void ScoreChange(int score);
    public static event ScoreChange OnChangeScore;
    public static event ScoreChange OnChangeJamCount;
    public static event ScoreChange OnChangeHp;
    public static event ScoreChange OnChangeBomb;
    public static event ScoreChange OnChangePower;

    // ����Ƽ Action // ��������Ʈ ���� ���� ü���� ����ų� ����
    public event Action OnDiedPlayer;
    public event Action<int, bool> OnPlayerDied;
    

    private int score = 0;
    private int curHp;
    private int maxHp;
    private int jamCount;
    private int bombCount;
    private int powerLevel;

    public int Score => score;
    public int CurHP => curHp;
    public int MaxHp => maxHp;
    public int PowerLevel => powerLevel;
    public int JamCount => jamCount;
    public int BombCount => bombCount;

    private int SetScore 
    { 
        set 
        { 
            score = value; 
            OnChangeScore?.Invoke(score);
        } 
    }



    public void InitScoreReset()
    {
        score = 0;
        OnChangeScore?.Invoke(score);

        curHp = maxHp = 5;
        OnChangeHp?.Invoke(curHp);

        powerLevel = 1;
        OnChangePower?.Invoke(powerLevel);

        jamCount = 0;
        OnChangeJamCount?.Invoke(jamCount);

        bombCount = 3;
        OnChangeBomb?.Invoke(bombCount);
    }

    private void OnEnable()
    {
        // ��������Ʈ�� ����
        Enemy.OnMonsterDied += HandleMonsterDied;
        DropItem_Jam.OnPickUpJam += HandleJamPickUp;
        PlayerHitbox.OnPlayerHpIncrease += PlayerHpChange;
    }
    
    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsterDied;
        DropItem_Jam.OnPickUpJam -= HandleJamPickUp;
        PlayerHitbox.OnPlayerHpIncrease -= PlayerHpChange;
    }

    // ��������Ʈ�� �ݹ� �޼ҵ�
    private void HandleMonsterDied(Enemy enemyInfo)
    {
        
    }

    private void HandleJamPickUp()
    {
        OnChangeJamCount?.Invoke(jamCount++);
        SetScore = Score + 7; 
    }

    public void PlayerHpChange(bool isIncreased)
    {
        if (isIncreased)
            IncreaseHp();
        else
            DecreaseHp();

        OnChangeHp?.Invoke(curHp);
    }

    private void IncreaseHp()
    {
        curHp++;
        if (curHp > maxHp)
            curHp = maxHp;

        // UI ����..
    }

    private void DecreaseHp()
    {
        curHp--;
        if(curHp < 1)
        {
            curHp = 0;

            OnDiedPlayer?.Invoke();
        }
    }

    public void ChangeBombCount(bool isIncreased)
    {
        if (isIncreased)
            bombCount++;
        else
            bombCount--;

        OnChangeBomb?.Invoke(bombCount);
    }

    public void PowerUp()
    {
        powerLevel++;

        OnChangePower?.Invoke(powerLevel);
    }
}

