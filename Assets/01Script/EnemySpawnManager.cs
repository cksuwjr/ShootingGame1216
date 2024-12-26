using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnTrans;
    [SerializeField] private GameObject[] spawnEnemyPrefabs;
    [SerializeField] private GameObject[] spawnbossPrefabs;

    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 0;
    private GameObject obj;


    public void InitSpawnManager()
    {
        spawnLevel = 0;
        spawnCount = 0;
        spawnDelta = 3f;

        StartCoroutine(SpawnEnemy());
    }

    public void StopSpawnManager()
    {
        StopAllCoroutines(); // �ش� ��ü�� ���ؼ� ������ ��� �ڷ�ƾ ����
    }

    // ���̵� ��½�Ű�� �޼ҵ�

    // ���� óġ�� ������ ȣ��(��������Ʈ ���)
    public void NextLevel()
    {
        StartCoroutine(SpawnEnemy());
    }


    IEnumerator SpawnEnemy()
    {
        yield return null;

        while(spawnCount < 10000)
        {
            for(int i = 0; i < spawnTrans.Length; i++) 
            { 
                obj = Instantiate(spawnEnemyPrefabs[spawnLevel], spawnTrans[i].position, Quaternion.identity);
                
                if(obj.TryGetComponent<Enemy>(out Enemy enemy))
                    enemy.SetEnable(true);
            }
            spawnCount++;
            yield return new WaitForSeconds(spawnDelta);
        }
    }


}
