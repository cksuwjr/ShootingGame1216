using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추상 클래스 : 추상 메서드를 가지고 있는 클래스를 가르키는 용어 => 단독 객체 생성 불가
// 만드는 이유 => 파생클래스를 생성하여 그 안에서 추상 메서드를 의무 구현하게 하기 위해서 => 다형성을 위해
// 추상 메서드 : 구현은 없고 선언만 있는

// 공통 기능
// 몬스터에서 드랍 되어 생성시 일정시간 한방향 이동하다(3~4초간) 방향 전환

// 플레이어와 충돌시 플레이어 습득 처리


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public abstract class FlyItemBase : MonoBehaviour, IMovement, IPickuped
{
    public abstract void ApplyEffect(GameObject target); // 구현부가 없는 추상메서드
    private bool isInit = false;
    private float flySpeed = 0.7f;
    private Vector2 flyDirection = Vector2.zero;
    private Vector3 flyTargetPos = Vector3.zero;

    private ScoreManager scoreManager;
    protected ScoreManager ScoreMGR
    {
        get
        {
            if (scoreManager == null)
                scoreManager = FindAnyObjectByType<ScoreManager>();
            return scoreManager;
        }
    }
    

    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0;
        }

        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.25f;
        }

        SetEnable(true);
    }

    private void Update()
    {
        if (isInit)
        {
            Move(flyDirection);
        }
    }

    public void Move(Vector2 newDirection)
    {
        transform.Translate(newDirection * (flySpeed * Time.deltaTime));
    }
    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
        if (newEnable)
        {
            StartCoroutine("ChangeFlyDirection");
        }
        else
        {
            StopCoroutine("ChangeFlyDirection");
        }
    }

    IEnumerator ChangeFlyDirection()
    {
        while (true)
        {
            flyTargetPos.x = Random.Range(-2f, 2f);
            flyTargetPos.y = Random.Range(-2f, 2f);
            flyTargetPos.z = 0;
            flyDirection = (flyTargetPos - transform.position).normalized;
            yield return new WaitForSeconds(4f);
        }
    }

    public void OnPickUp(GameObject picker)
    {
        ApplyEffect(picker);
        Destroy(gameObject);
    }

}
