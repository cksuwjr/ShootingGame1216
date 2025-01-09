using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    private Animator anims;

    [SerializeField] private AnimationCurve curve;

    private Vector3 startPos;
    private Vector3 endPos = Vector3.zero;

    private float current;
    private float percent;

    private void Awake()
    {
        TryGetComponent<Animator>(out anims);

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {

        startPos = FindAnyObjectByType<PlayerWeapon>().transform.position;

        current = 0f;
        percent = 0f;

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1.5f; // 출발지 -> 목표지까지 1.5초동안 이동시키기 위함

            // 시작 -> 목표좌표까지 선형 보간
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(percent));
            yield return null;
        }
        anims.SetTrigger("OnFire");
        yield return new WaitForSeconds(1.9f);
        OnFire();
        yield return null;
        Destroy(gameObject);
    }

    private void OnFire()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < enemys.Length; i++)
        {
            if(enemys[i].TryGetComponent<IDamaged>(out IDamaged damaged))
            {
                damaged.TakeDamage(null, 100);
            }
        }

        //foreach (GameObject enemy in enemys)
        //{

        //}
    }
}
