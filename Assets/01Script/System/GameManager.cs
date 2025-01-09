using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� �帧�� ����
// 1.������ ����, ����, ����
// 2.���� ������
// 3.�����ε� : ���� �ε�
// 4.�� ���� : �� ���� ����
// 5.�Է� �ý���
public class GameManager : Singleton<GameManager>
{
    private PlayerController playerController;

    private ScoreManager scoreManager;
    private ScrollManager scrollManager;
    private EnemySpawnManager enemySpawnManager;
    private MeteoManager meteoManager;

    public ScoreManager GetScoreManager => scoreManager;

    private IInputHandle inputHandle;

    // ���� ������ �Ǿ �ε��� ������ ��
    // �ѹ� �� ȣ���� �ǵ���
    private void Start()
    {
        LoadSceneInit(); // �ӽ� : ���� �� ���� ���� �Ϸ��ϰ� ���� ����
        StartCoroutine(GameStart()); // �ӽ�
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

    // ���� ���۵� �� ó���Ǿ�� �ϴ� �Ϸ��� �������� ������ ���� �������ִ� ����
    IEnumerator GameStart()
    {
        yield return null;
        scoreManager?.InitScoreReset();
        Debug.Log("������ �ʱ�ȭ");
        Debug.Log("BGM ����");
        yield return new WaitForSeconds(1);
        playerController?.StartGame();
        scrollManager?.SetScrollSpeed(4f);
        yield return new WaitForSeconds(2f);
        enemySpawnManager?.InitSpawnManager();
        yield return new WaitForSeconds(10f);
        meteoManager?.StartSpawnMeteo();

    }
}
