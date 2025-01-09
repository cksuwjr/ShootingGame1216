using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollecter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUpItem"))
        {
            if(collision.TryGetComponent<IPickuped>(out IPickuped pickuped))
            {
                pickuped.OnPickUp(transform.root.gameObject);
            }
        }
    }
}
