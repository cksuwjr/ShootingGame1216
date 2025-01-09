using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertLine : MonoBehaviour
{
    [SerializeField] private GameObject prefabMeteo;

    private Animator anims;
    private Animator Anims
    {
        get => anims == null ? GetComponent<Animator>() : anims;
    }

    // 월드에 라인 최초 생성시
    public void SpawnedLine()
    {
        //Anims.SetTrigger("Spawn");
        Invoke("SpawnMeteo", 2.0f);
    }

    private void SpawnMeteo()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y = 8.0f;
        GameObject obj = Instantiate(prefabMeteo, spawnPos, Quaternion.identity);
        if(obj.TryGetComponent<Meteorite>(out Meteorite meteorite))
        {
            meteorite.InitMeteo();
            Destroy(gameObject);
        }
    }
}
