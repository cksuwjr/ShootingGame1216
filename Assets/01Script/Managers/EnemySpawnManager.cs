using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private ShootingGame gameDatas;
    private Dictionary<int, MonsterTable_Entity> monsterDatas = new Dictionary<int, MonsterTable_Entity> ();

    [SerializeField] private Transform[] spawnTrans;
    [SerializeField] private GameObject[] spawnEnemyPrefabs;
    [SerializeField] private GameObject[] spawnbossPrefabs;

    private Queue<Enemy>[] enemysQueue;
    private int poolSize = 5;
    private Enemy enemy;


    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 0;
    private GameObject obj;

    private EnemyBoss bossAI;

    public delegate void SpawnFinish();

    public static event SpawnFinish OnSpawnFinish;

    /// //////////
    private void Awake()
    {
        for(int i = 0; i < gameDatas.MonsterTable.Count; i++)
        {
            monsterDatas.Add(gameDatas.MonsterTable[i].MonsterID, gameDatas.MonsterTable[i]);
        }
        
        enemysQueue = new Queue<Enemy>[spawnEnemyPrefabs.Length];
        for(int i = 0; i < spawnEnemyPrefabs.Length; i++)
        {
            enemysQueue[i] = new Queue<Enemy>();
        }
    }


    private void Allocate(int n)
    {
        for(int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(spawnEnemyPrefabs[n]);
            if(obj.TryGetComponent<Enemy>(out enemy))
            {
                enemysQueue[n].Enqueue(enemy);
            }
            obj.SetActive(false);
        }
    }

    private Enemy GetEnemyFromPool(int n)
    {
        if (enemysQueue[n].Count < 1)
        {
            Allocate(n);
        }
        return enemysQueue[n].Dequeue();
    }

    public void ReturnEnemyToPool(Enemy returnEnemy, int n)
    {
        returnEnemy.gameObject.SetActive(false);
        enemysQueue[n].Enqueue(returnEnemy);
    }

    /// /////////

    public void InitSpawnManager()
    {
        spawnLevel = 0;
        spawnCount = 0;
        spawnDelta = 3f;

        StartCoroutine(SpawnEnemy());
    }

    public void StopSpawnManager()
    {
        StopAllCoroutines(); // 해당 객체를 통해서 생성한 모든 코루틴 정지
    }


    IEnumerator SpawnEnemy()
    {
        yield return null;

        while(spawnCount < GameState.waveCount)
        {
            for(int i = 0; i < spawnTrans.Length; i++) 
            {
                //obj = Instantiate(spawnEnemyPrefabs[spawnLevel], spawnTrans[i].position, Quaternion.identity);
                obj = GetEnemyFromPool(spawnLevel).gameObject;
                obj.transform.position = spawnTrans[i].position;
                obj.SetActive(true);

                if (obj.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    int tableID = 10000
                        + 10 * (spawnLevel / 3 + 1)
                        + 1 * (spawnLevel % 3 + 1);

                    if (monsterDatas.TryGetValue(tableID, out MonsterTable_Entity data))
                    {
                        enemy.InitMonsterData(data);
                    }
                    enemy.SetEnable(true);
                }
            }
            spawnCount++;
            yield return new WaitForSeconds(spawnDelta);
        }
        // 일반 몬스터 10번 등장 종료
        // 유저 경고 메시지 
        yield return new WaitForSeconds(3f);

        obj = Instantiate(spawnbossPrefabs[spawnLevel], new Vector3(0f, 8f, 0f), Quaternion.identity);

        if(obj.TryGetComponent<EnemyBoss>(out bossAI))
        {
            IWeapon[] weapons = new IWeapon[] { new BossWeapon_3(), new BossWeapon_2() };

            for(int i = 0;i < weapons.Length; i++)
            {
                weapons[i].SetOwner(obj);
            }
            bossAI.InitBoss("무지막지한 보스", 500, weapons);
            bossAI.OnBossDie += NextLevel;
        }
        // 보스등장
    }

    // 난이도 상승시키는 메소드

    // 보스 처치할 떄마다 호출(델리게이트 방식)
    public void NextLevel()
    {
        bossAI.OnBossDie -= NextLevel;

        spawnLevel++;
        if(spawnLevel > 3)
        {
            spawnLevel = 0;
        }
        spawnCount = 0;

        StartCoroutine(SpawnEnemy());
    }


}
