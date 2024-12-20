using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2종류의 싱글톤 베이스
// 1. 씬이 변경되더라도 인스턴스가 유지가 되는 싱글톤
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

    protected virtual void DoAwake() { } // 파생 클래스에서 초기화 로직 작성시


}
    // 씬이 변경되면 인스턴스가 파괴되는 싱글톤

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
