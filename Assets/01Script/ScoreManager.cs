using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    private void OnEnable()
    {
        // 델리게이트의 구독
        Enemy.OnMonsterDied += HandleMonsterDied;
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsterDied;
    }

    // 델리게이트의 콜백 메소드
    private void HandleMonsterDied(Enemy enemyInfo)
    {
        score += 1;
        Debug.Log($"점수 : {score}");
    }
}
