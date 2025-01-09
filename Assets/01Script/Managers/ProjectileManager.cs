using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : SingletonDestroy<ProjectileManager>
{
    [SerializeField] private GameObject[] projectilePrefabs;
    private Queue<Projectile>[] projectilesQueue;

    private int poolSize = 10;
    private GameObject obj;
    private Projectile proj;

    protected override void Awake()
    {
        base.Awake();

        projectilesQueue = new Queue<Projectile>[projectilePrefabs.Length];

        for(int i = 0;  i < projectilePrefabs.Length; i++)
        {
            projectilesQueue[i] = new Queue<Projectile>();
        }
    }

    private void Allocate(ProjectileType type)
    {
        for (int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(projectilePrefabs[(int)type]);
            if(obj.TryGetComponent<Projectile>(out proj))
            {
                projectilesQueue[(int)type].Enqueue(proj);
            }
            obj.SetActive(false);
        }
    }

    // 풀에서 오브젝트 꺼내올떄

    private Projectile GetProjectileFromPool(ProjectileType type)
    {
        if (projectilesQueue[(int)type].Count < 1)
        {
            Allocate(type);
        }
        return projectilesQueue[(int)type].Dequeue();
    }

    // 사용되다가 필요없어졌을때 다시 반환
    public void ReturnProjectileToPool(Projectile returnProj, ProjectileType type)
    {
        returnProj.gameObject.SetActive(false);
        projectilesQueue[(int)type].Enqueue(returnProj);
    }

    // 외부에서 요청이 올때 프로젝타일 생성해서 발사

    public void FireProjectile(ProjectileType type, Vector3 spawnPos, Vector2 newDir, 
        GameObject newOwner, int damage, float newSpeed)
    {
        proj = GetProjectileFromPool(type);
        if(proj != null)
        {
            proj.transform.position = spawnPos;
            proj.gameObject.SetActive(true);
            proj.InitProjectile(type, newDir, newOwner, damage, newSpeed);
        }
    }
}
