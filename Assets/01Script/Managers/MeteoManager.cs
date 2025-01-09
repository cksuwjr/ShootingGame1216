using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 경고 라인을 생성해서 메테오가 떨어지도록 만들어주는 역할
// 경고 라인을 스폰
// 게임의 진행도에 따라 난이도를 상승시키는 역할

public class MeteoManager : MonoBehaviour
{
    [SerializeField] private GameObject alertLinePrefabs;
    private float spawnDelta = 3f; // 처음은 3초, 10개정도 생성시 0.2초씩 감소하도록 만들예정
    private int countSpawnMeteo = 0;
    private GameObject obj;
    private AlertLine alertLine;
    private Vector3 spawnPos = Vector3.zero;
    private bool isInit = false;

    public void StartSpawnMeteo()
    {
        StartCoroutine("SpawnMeteo");
    }

    public void StopSpawnMeteo()
    {
        StopCoroutine("SpawnMeteo");
    }

    IEnumerator SpawnMeteo()
    {
        while (true)
        {
            spawnPos.x = Random.Range(-2.2f, 2.2f);
            
            obj = Instantiate(alertLinePrefabs, spawnPos, Quaternion.identity);
            if(obj.TryGetComponent<AlertLine>(out AlertLine alertLine))
            {
                alertLine.SpawnedLine();
            }
            countSpawnMeteo++;
            if(countSpawnMeteo % 10 == 0)
            {
                spawnDelta -= 0.2f;
                spawnDelta = Mathf.Max(1.5f, spawnDelta);
            }
            yield return new WaitForSeconds(spawnDelta);
        }
    }
}
