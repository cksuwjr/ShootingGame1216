using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���Ͱ� ����ߴٴ� �ν��� �ϰ� �Ǹ�
// �ش��ϴ� �������� ���
public class ItemDropManager : MonoBehaviour
{
    [SerializeField] private GameObject jamPrefab;
    [SerializeField] private List<GameObject> flyItems;

    private GameObject obj;

    private int dropRate;

    private void OnEnable()
    {
        Enemy.OnMonsterDied += HandleEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleEnemyDied;
    }
    
    private void HandleEnemyDied(Enemy enemyInfo)
    {
        for(int i = 0; i < 7; i++)
        {
            obj = Instantiate(jamPrefab, enemyInfo.transform.position, Quaternion.identity);
        }
        dropRate = Random.Range(0, 1000);

        if(dropRate < 10)
            obj = Instantiate(flyItems[0], enemyInfo.transform.position, Quaternion.identity); 
        else if(dropRate < 20)
            obj = Instantiate(flyItems[1], enemyInfo.transform.position, Quaternion.identity);
        else if (dropRate < 30)
            obj = Instantiate(flyItems[2], enemyInfo.transform.position, Quaternion.identity);


    }
}
