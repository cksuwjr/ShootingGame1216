using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 데미지 받는 일반적인 적기의 기능
// 2. AI : 인공지능 FSM 유한상태기계

public enum BossState
{
    BS_MoveToAppear, // 등장과정 (전투 시작 전 전투 위치로 이동하고 있는 상태)
    BS_Phase01, // 제자리 공격 반복
    BS_Phase02, // 좌우로 이동하면서 공격
}

// 다형성을 이용해서 공격방식을 구현한다
// 여러 웨폰을 가지고 있고, 각각의 웨폰이 서로 다른 방식의 공격을 구현한다.
// 웨폰을 활성화 해주는 캐릭터

public class EnemyBoss : MonoBehaviour, IMovement, IDamaged
{
    [SerializeField] private float bossAppearPointY = 2.5f;
    private BossState currentState = BossState.BS_MoveToAppear;
    private IWeapon[] weapons;
    private IWeapon currentWeapon;

    private Vector2 moveDir = Vector2.zero;

    private bool isInit = false;
    private float moveSpeed = 3f;
    private string bossName;
    private int maxHp;
    private int curHp;

    public bool IsDead => curHp <= 0;

    public delegate void BossDieEvent();
    public event BossDieEvent OnBossDie;

    public void InitBoss(string name, int newHp, IWeapon[] newWeapons)
    {
        bossName = name;
        curHp = maxHp = newHp; 

        weapons = newWeapons;

        SetEnable(true);

        ChangeState(BossState.BS_MoveToAppear);
    }

    // 전처리기 하나의 종류

    #region _AICord_
    // 상태값을 변경시켜주는 메서드
    public void ChangeState(BossState newState)
    {
        StopCoroutine(currentState.ToString()); // 기존 상태값의 코드를 멈추고
        currentState = newState;
        StartCoroutine(currentState.ToString());
    }

    private IEnumerator BS_MoveToAppear()
    {
        moveDir = Vector2.down;
        while (true)
        {
            if(transform.position.y <= bossAppearPointY)// 전투 위치에 도달했는가
            {
                moveDir = Vector2.zero;
                ChangeState(BossState.BS_Phase01);
            }
            yield return null;
        }
    }

    // 제자리에서 무기 작동
    private IEnumerator BS_Phase01 ()
    {
        currentWeapon = weapons[0];
        currentWeapon.SetEnable(true);
        while(true)
        {
            currentWeapon.Fire();
            yield return new WaitForSeconds(0.7f);
        }
    }
    private IEnumerator BS_Phase02()
    {
        currentWeapon.SetEnable(false);
        currentWeapon = weapons[1];
        currentWeapon.SetEnable(true);

        moveDir = Vector2.right;
        while (true)
        {
            currentWeapon.Fire();
            if(transform.position.x <= -2.5f || transform.position.x >= 2.5f)
            {
                moveDir *= -1f;
            }
            yield return new WaitForSeconds(0.4f);
        }
    }
    #endregion

    private void Update()
    {
        if (isInit)
            Move(moveDir);
    }

    public void Move(Vector2 newDirection)
    {
        transform.Translate(moveDir * (moveSpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if (!IsDead)
        {
            curHp -= damage;
            if(curHp > 0)
            {// 피격
                OnDamaged();
            }
            else
            {// 사망
                OnDied();
            }
        }
    }

    private void OnDamaged()
    {
        if(currentState == BossState.BS_Phase01 && (float)curHp/maxHp < 0.5f)
        {
            ChangeState(BossState.BS_Phase02); 
        }
    }

    private void OnDied()
    {
        OnBossDie?.Invoke();
        Destroy(gameObject);
    }
}
