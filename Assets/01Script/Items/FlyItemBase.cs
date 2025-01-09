using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߻� Ŭ���� : �߻� �޼��带 ������ �ִ� Ŭ������ ����Ű�� ��� => �ܵ� ��ü ���� �Ұ�
// ����� ���� => �Ļ�Ŭ������ �����Ͽ� �� �ȿ��� �߻� �޼��带 �ǹ� �����ϰ� �ϱ� ���ؼ� => �������� ����
// �߻� �޼��� : ������ ���� ���� �ִ�

// ���� ���
// ���Ϳ��� ��� �Ǿ� ������ �����ð� �ѹ��� �̵��ϴ�(3~4�ʰ�) ���� ��ȯ

// �÷��̾�� �浹�� �÷��̾� ���� ó��


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public abstract class FlyItemBase : MonoBehaviour, IMovement, IPickuped
{
    public abstract void ApplyEffect(GameObject target); // �����ΰ� ���� �߻�޼���
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
