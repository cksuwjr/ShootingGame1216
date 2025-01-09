using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. ������ �޴� �Ϲ����� ������ ���
// 2. AI : �ΰ����� FSM ���ѻ��±��

public enum BossState
{
    BS_MoveToAppear, // ������� (���� ���� �� ���� ��ġ�� �̵��ϰ� �ִ� ����)
    BS_Phase01, // ���ڸ� ���� �ݺ�
    BS_Phase02, // �¿�� �̵��ϸ鼭 ����
}

// �������� �̿��ؼ� ���ݹ���� �����Ѵ�
// ���� ������ ������ �ְ�, ������ ������ ���� �ٸ� ����� ������ �����Ѵ�.
// ������ Ȱ��ȭ ���ִ� ĳ����

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

    // ��ó���� �ϳ��� ����

    #region _AICord_
    // ���°��� ��������ִ� �޼���
    public void ChangeState(BossState newState)
    {
        StopCoroutine(currentState.ToString()); // ���� ���°��� �ڵ带 ���߰�
        currentState = newState;
        StartCoroutine(currentState.ToString());
    }

    private IEnumerator BS_MoveToAppear()
    {
        moveDir = Vector2.down;
        while (true)
        {
            if(transform.position.y <= bossAppearPointY)// ���� ��ġ�� �����ߴ°�
            {
                moveDir = Vector2.zero;
                ChangeState(BossState.BS_Phase01);
            }
            yield return null;
        }
    }

    // ���ڸ����� ���� �۵�
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
            {// �ǰ�
                OnDamaged();
            }
            else
            {// ���
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
