using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 월드에 있는 특정 interface를 찾아서 탐색해오는 기능
public class InterfaceFinder : MonoBehaviour
{
    // 월드에 있는 모든 오브젝트를 검사하여 T타입을 발견
    public static List<T> FindObjectsOfInterface<T>() where T : class
    {
        // FindObjectsByType : 성능이 좋고, 정렬기능 등을 제공
        // FindObjectsOfType : 성능이 좋지않고, 정렬이나 추가적 탐색 옵션이 제공되지 않음
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        List<T> interfaceObjects = new List<T>();


        // 제네릭 프로그래밍 문법 var
        foreach(var obj in allObjects)
        {
            if(obj is T interfaceObject) // obj가 T타입이면 타입캐스팅하여 interfaceObject라 참조하겠다
            {
                interfaceObjects.Add(interfaceObject);
            }
        }

        return interfaceObjects;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
