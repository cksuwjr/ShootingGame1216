using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 전반 흐름을 관리
// 1.게임의 시작, 중지, 종료
// 2.사운드 관리자
// 3.동적로딩 : 에셋 로딩
// 4.씬 관리 : 씬 변경 관리
// 5.입력 시스템
public class GameManager : Singleton<GameManager>
{
    private PlayerController playerController;

    private ScoreManager scoreManager;
    private ScrollManager scrollManager;
    private EnemySpawnManager enemySpawnManager;
    private MeteoManager meteoManager;

    public ScoreManager GetScoreManager => scoreManager;

    private IInputHandle inputHandle;

    // 씬이 변경이 되어서 로딩이 끝났을 때
    // 한번 씩 호출이 되도록
    private void Start()
    {
        LoadSceneInit(); // 임시 : 추후 씬 변경 로직 완료하고 나서 수정
        StartCoroutine(GameStart()); // 임시
    }

    private void LoadSceneInit()
    {
        playerController = FindAnyObjectByType<PlayerController>();

        scoreManager = FindAnyObjectByType<ScoreManager>();
        scrollManager = FindAnyObjectByType<ScrollManager>();
        enemySpawnManager = FindAnyObjectByType<EnemySpawnManager>();
        inputHandle = GetComponent<KeyboardInputHandle>();
        meteoManager = FindAnyObjectByType<MeteoManager>();
        //switch (inputType)
        //{
        //    case 1:
        //        inputHandle = 
        //}
    }

    private void Update()
    {
        if (inputHandle != null)
        {
            playerController?.CustomUpdate(inputHandle.GetInput());
        }
    }

    // 게임 시작될 떄 처리되어야 하는 일련의 로직들을 순서에 맞춰 수행해주는 역할
    IEnumerator GameStart()
    {
        yield return null;
        scoreManager?.InitScoreReset();
        Debug.Log("데이터 초기화");
        Debug.Log("BGM 시작");
        yield return new WaitForSeconds(1);
        playerController?.StartGame();
        scrollManager?.SetScrollSpeed(4f);
        yield return new WaitForSeconds(2f);
        enemySpawnManager?.InitSpawnManager();
        yield return new WaitForSeconds(10f);
        meteoManager?.StartSpawnMeteo();

    }
}
