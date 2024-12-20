using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaged
{
    void TakeDamage(GameObject attacker, int damage);
}
