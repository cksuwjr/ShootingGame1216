using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void SetEnable(bool newEnable); // �̵� ����/�Ұ��� set

    void Move(Vector2 newDirection); // 
}
