using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��� ������ �����ؼ� ���׿��� ���������� ������ִ� ����
// ��� ������ ����
// ������ ���൵�� ���� ���̵��� ��½�Ű�� ����

public class MeteoManager : MonoBehaviour
{
    [SerializeField] private GameObject alertLinePrefabs;
    private float spawnDelta = 3f; // ó���� 3��, 10������ ������ 0.2�ʾ� �����ϵ��� ���鿹��
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
