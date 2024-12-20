using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 여러 타입의 무기를 만들고
// 플레이어 혹은 몬스터가 활용하기 위함
public class PlayerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject projectilePrefab; // 나중에 오브젝트 풀링 구현하면서 수정할 예정
    [SerializeField] private Transform firePoint;

    private int numOfProjectile = 5; // 발사 수량
    private float spreadAngle = 5f; // 투사체 발사 각도 간격
    private float fireRate = 0.3f; // 발사 시간 간격
    private float nextFireRate = 0f;
    private float nextFireTime = 0f;
    private bool isFiring = false;

    private float startAngle;
    private float angle;
    private Quaternion fireRotation;
    private Vector2 fireDir;
    private GameObject obj;


    public void Fire()
    {
        if (Time.time < nextFireTime)
            return;
        if (isFiring)
        {
            nextFireTime = Time.time + fireRate;

            // 첫번째 프로젝타일 발사하는 각도.
            startAngle = -spreadAngle * (numOfProjectile - 1) / 2;

            for(int i = 0; i < numOfProjectile; i++)
            {
                angle = startAngle + spreadAngle * i;

                fireRotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
                fireDir = fireRotation * Vector2.up;

                ProjectileManager.Instance.FireProjectile(ProjectileType.Player01, firePoint.position, fireDir, gameObject, 1, 10f);
            }
        }
    }

    public void SetEnable(bool enable)
    {
        isFiring = enable;
    }

    public void SetOwner(GameManager newOwner)
    {
        
    }
}
