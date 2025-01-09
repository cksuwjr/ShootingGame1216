using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// 배치되면 플레이어 방해
// 이동 기능
// 데미지 받는 기능
// 사망시 플레이어 리워드(델리게이트/콜백) 부여 기능

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IMovement, IDamaged
{
    private Vector2 moveDir;
    private float moveSpeed = 3f;
    private bool isInit = false;
    private int curHp = 10;
    private int maxHp = 10;

    public EnemySpawnManager spawnManager;

    public bool isDead { get => curHp < 0; }

    public delegate void MonsterDiedEvent(Enemy enemyInfo); // 델리게이트 타입 선언
    public static event MonsterDiedEvent OnMonsterDied; // 델리게이트 체인 선언 (ex 구독자 리스트
    // event << public 선언된 델리게이트 체인에 접근하여 구독할 순 있지만, 데이터를 변형시킬수 없도록 선언한 방식.



    private void Awake()
    {
        if (TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.25f;
        }

        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0;
        }
    }

    private void Update()
    {
        if (isInit)
        {
            Move(Vector2.down);

            if(transform.position.y < -7)
            {
                gameObject.SetActive(false);
                spawnManager?.ReturnEnemyToPool(this, 0);
                //Destroy(gameObject);
            }
        }
    }

    public void InitMonsterData(MonsterTable_Entity newData)
    {
        maxHp = newData.MonsterHP;
        curHp = maxHp;

    }

    public void Move(Vector2 newDirection)
    {
        transform.Translate(newDirection * (moveSpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if(!isDead)
        {
            curHp -= damage;
            if(curHp < 1)
                OnDie();
            else
                OnDamaged();

        }
    }

    private void OnDamaged()
    {

    }

    private void OnDie()
    {
        OnMonsterDied?.Invoke(this);
        SetEnable(false);
        gameObject.SetActive(false);
        spawnManager?.ReturnEnemyToPool(this, 0);
        //Destroy(gameObject);
    }

}
