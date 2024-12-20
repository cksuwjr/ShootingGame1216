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
    private ScrollManager scrollManager;
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
        scrollManager = FindAnyObjectByType<ScrollManager>();
        inputHandle = GetComponent<KeyboardInputHandle>();

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
        Debug.Log("������ �ʱ�ȭ");
        Debug.Log("BGM ����");
        yield return new WaitForSeconds(1);
        playerController?.StartGame();
        scrollManager?.SetScrollSpeed(4f);
        Debug.Log("���� ���� ����");

    }
}
