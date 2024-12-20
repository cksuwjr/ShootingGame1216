using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� Ÿ���� ���⸦ �����
// �÷��̾� Ȥ�� ���Ͱ� Ȱ���ϱ� ����
public class PlayerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject projectilePrefab; // ���߿� ������Ʈ Ǯ�� �����ϸ鼭 ������ ����
    [SerializeField] private Transform firePoint;

    private int numOfProjectile = 5; // �߻� ����
    private float spreadAngle = 5f; // ����ü �߻� ���� ����
    private float fireRate = 0.3f; // �߻� �ð� ����
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

            // ù��° ������Ÿ�� �߻��ϴ� ����.
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
