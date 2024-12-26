using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    private void OnEnable()
    {
        // ��������Ʈ�� ����
        Enemy.OnMonsterDied += HandleMonsterDied;
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsterDied;
    }

    // ��������Ʈ�� �ݹ� �޼ҵ�
    private void HandleMonsterDied(Enemy enemyInfo)
    {
        score += 1;
        Debug.Log($"���� : {score}");
    }
}
