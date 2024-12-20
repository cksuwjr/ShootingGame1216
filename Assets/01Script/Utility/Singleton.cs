using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2������ �̱��� ���̽�
// 1. ���� ����Ǵ��� �ν��Ͻ��� ������ �Ǵ� �̱���
// 2. 
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public T Instance { get; private set; }

    protected virtual void Awake()
    {
        if(Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);

            DoAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void DoAwake() { } // �Ļ� Ŭ�������� �ʱ�ȭ ���� �ۼ���


}
    // ���� ����Ǹ� �ν��Ͻ��� �ı��Ǵ� �̱���

public class SingletonDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void DoAwake() { }
}
