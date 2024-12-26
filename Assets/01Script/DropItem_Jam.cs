using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DropItem_Jam : MonoBehaviour, IPickuped
{
    public delegate void PickUpJam();

    public static event PickUpJam OnPickUpJam;

    private Rigidbody2D rig;

    private bool isSetTarget = false;
    private GameObject target;
    private float pickupTimePer;

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.radius = 0.2f;
            col.isTrigger = true;
        }

        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 1.0f;

            Vector2 initVelocity = Vector2.zero;
            initVelocity.x = Random.Range(-0.5f, 0.5f);
            initVelocity.y = Random.Range(3.0f, 4.0f);
            rig.AddForce(initVelocity, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if(isSetTarget && target.activeSelf)
        {
            pickupTimePer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.transform.position, pickupTimePer / 2.0f);

            if(pickupTimePer / 2f > 1.0f)
            {
                OnPickUpJam?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void OnPickUp(GameObject picker)
    {
        rig.gravityScale = 0;
        rig.velocity = Vector2.zero;
        isSetTarget = true;
        target = picker;
        pickupTimePer = 0f;
    }

}
