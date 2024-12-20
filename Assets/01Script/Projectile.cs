using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지정한 방향으로 일정속도 지속이동
// 발사 주체와 다른 대상과 부딪히면 상대방에게 데미지 전달

public enum ProjectileType
{
    Player01, Player02, Player03,
    Boss01, Boss02, Boss03,
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class Projectile : MonoBehaviour, IMovement
{
    [SerializeField] private float moveSpeed = 10f;

    private int damage;
    private Vector2 moveDir;
    private GameObject owner;
    private string ownerTag; // 피아 식별 위한 태그

    private bool isInit = false;
    private ProjectileType type;

    private void Awake()
    {
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0f;
        }
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.1f;
        }
    }

    public void InitProjectile(ProjectileType type, Vector2 newDir, 
        GameObject newOwner, int newDamage, float newSpeed)
    {
        this.type = type;
        moveDir = newDir;
        moveSpeed = newSpeed;
        damage = newDamage;
        owner = newOwner;
        ownerTag = newOwner.tag;

        isInit = true;
    }

    private void OnDisable()
    {
        SetEnable(false);
    }

    private void Update()
    {
        if (isInit)
            Move(moveDir);
    }

    public void Move(Vector2 newDirection)
    {
        transform.Translate(newDirection * (moveSpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;
        if(collision.CompareTag(ownerTag)) return;

        if (collision.CompareTag("DestroyArea"))
        {
            ProjectileManager.Instance.ReturnProjectileToPool(this, type);
            return;
        }

        if(collision.TryGetComponent<IDamaged>(out IDamaged damaged))
        {
            damaged.TakeDamage(owner, damage);
            ProjectileManager.Instance.ReturnProjectileToPool(this, type);
        }
    }
}
