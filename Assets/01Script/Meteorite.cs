using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Meteorite : MonoBehaviour
{
    private Rigidbody2D rig;

    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 1.0f;
        }

        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.3f;
        }
    }

    public void InitMeteo()
    {
        rig.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.TryGetComponent<IDamaged>(out IDamaged damaged))
            {
                damaged.TakeDamage(gameObject, 1);
                Destroy(gameObject);
            }
        }
    }
}
