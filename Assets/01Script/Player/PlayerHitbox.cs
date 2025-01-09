using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 피격했다는 체크만
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerHitbox : MonoBehaviour, IDamaged
{
    public static Action<bool> OnPlayerHpIncrease; // true 회복, false 감소

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.2f;
        }

        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
            rig.gravityScale = 0;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        OnPlayerHpIncrease?.Invoke(false);
    }
}
