using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���忡 �ִ� Ư�� interface�� ã�Ƽ� Ž���ؿ��� ���
public class InterfaceFinder : MonoBehaviour
{
    // ���忡 �ִ� ��� ������Ʈ�� �˻��Ͽ� TŸ���� �߰�
    public static List<T> FindObjectsOfInterface<T>() where T : class
    {
        // FindObjectsByType : ������ ����, ���ı�� ���� ����
        // FindObjectsOfType : ������ �����ʰ�, �����̳� �߰��� Ž�� �ɼ��� �������� ����
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        List<T> interfaceObjects = new List<T>();


        // ���׸� ���α׷��� ���� var
        foreach(var obj in allObjects)
        {
            if(obj is T interfaceObject) // obj�� TŸ���̸� Ÿ��ĳ�����Ͽ� interfaceObject�� �����ϰڴ�
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
