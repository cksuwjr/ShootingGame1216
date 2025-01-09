using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMovement
{
    private bool isMoving = false; // true �����϶��� �̵�����.

    [SerializeField] private float moveSpeed = 5f; // �⺻�� 5

    // ĳ���� �̵����� ����
    private Vector2 minArea = new Vector2(-2f, -4.5f);
    private Vector2 maxArea = new Vector2(2f, 0f);

    // �̵��� ����� ���� ����
    private Vector3 moveDelta;

    public void Move(Vector2 newDirection)
    {
        if (isMoving)
        {
            moveDelta.x = newDirection.x;
            moveDelta.y = newDirection.y;
            moveDelta.z = 0;

            moveDelta *= (moveSpeed * Time.deltaTime);
            moveDelta += transform.position;

            moveDelta.x = Mathf.Clamp(moveDelta.x, minArea.x, maxArea.x);
            moveDelta.y = Mathf.Clamp(moveDelta.y, minArea.y, maxArea.y);

            transform.position = moveDelta;
        }
    }

    public void SetEnable(bool newEnable)
    {
        isMoving = newEnable;
    }
}
