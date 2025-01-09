using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossWeaponBase
{
    protected GameObject owner;
}

public class BossWeapon_1 : BossWeaponBase, IWeapon
{
    private Vector3 firePos;
    private int numOfProj = 5;
    private float spreadAngle = 15f;

    private float angle;
    private Vector2 fireDir;

    public void Fire()
    {
        firePos = owner.transform.position;
        for(int i = 0; i < numOfProj; i++)
        {
            angle = spreadAngle * (i - (numOfProj - 1) / 2f);
            fireDir = Quaternion.Euler(0, 0, angle) * Vector2.down;
            ProjectileManager.Instance.FireProjectile(ProjectileType.Boss01, firePos, fireDir, owner, 1, 6f);
        }

    }

    public void LunchBomb()
    {
        throw new System.NotImplementedException();
    }

    public void SetEnable(bool enable)
    {
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}

public class BossWeapon_2 : BossWeaponBase, IWeapon
{
    private Vector3 firePos;
    private int numOfProj = 36;
    private float angleDelta;
    private float startAngle; // 매번 같은 방향으로 발사되는걸 방지
    private int count = 0;

    private float spawnAngle;
    private Vector2 fireDir;
    public void Fire()
    {
        firePos = owner.transform.position;
        angleDelta = 360f / numOfProj;

        startAngle = count++ * 1.1f;

        for(int i = 0; i < numOfProj; i++)
        {
            spawnAngle = i * angleDelta + startAngle;
            fireDir = Quaternion.Euler(0f, 0f, spawnAngle) * Vector2.down;
            ProjectileManager.Instance.FireProjectile(ProjectileType.Boss02, firePos, fireDir, owner, 1, 4f);
        }
    }

    public void LunchBomb()
    {
    }

    public void SetEnable(bool enable)
    {
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}

public class BossWeapon_3 : BossWeaponBase, IWeapon
{
    private Vector3 firePos;
    private int numOfProj = 1;
    private float angleDelta;
    private float startAngle; // 매번 같은 방향으로 발사되는걸 방지
    private int count = 0;

    private float spawnAngle;
    private Vector2 fireDir;

    // add
    private float proj1Speed = 3f;
    public void Fire()
    {
        firePos = owner.transform.position;
        angleDelta = 360 / numOfProj;

        count++;
        startAngle = angleDelta + count * 3;
        proj1Speed -= count * 0.026f;
        if (proj1Speed < 0.2f) proj1Speed = 8;

        for (int i = 0; i < numOfProj; i++)
        {
            spawnAngle = startAngle + i * angleDelta;
            fireDir = Quaternion.Euler(0f, 0f, spawnAngle) * Vector2.down;
            ProjectileManager.Instance.FireProjectile(ProjectileType.Boss01, firePos, fireDir, owner, 1, proj1Speed);
        }
        numOfProj += 2;
        numOfProj = Mathf.Min(numOfProj, 30);


        spawnAngle = count * 8;
        spawnAngle %= 180;
        var proj2Speed = 0.6f - 0.001f * spawnAngle;

        fireDir = Quaternion.Euler(0f, 0f, spawnAngle) * Vector2.down;
        ProjectileManager.Instance.FireProjectile(ProjectileType.Boss01, firePos, fireDir, owner, 1, proj2Speed);

        fireDir = Quaternion.Euler(0f, 0f, -spawnAngle) * Vector2.down;
        ProjectileManager.Instance.FireProjectile(ProjectileType.Boss01, firePos, fireDir, owner, 1, proj2Speed);

    }

    public void LunchBomb()
    {
    }

    public void SetEnable(bool enable)
    {
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}

public class EnemyBoss_Weapon : MonoBehaviour
{

}
