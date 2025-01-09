using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �÷��̾����Ŭ
public class PlayerController : MonoBehaviour
{
    private IMovement movement;
    private IWeapon weapon;

    private void Awake()
    {
        if (!TryGetComponent<IMovement>(out movement))
            Debug.Log("PlayerController.cs - Awake() - movement���� ����");
        if(!TryGetComponent<IWeapon>(out weapon))
            Debug.Log("PlayerController.cs - Awake() - weapon���� ����");
    }

    public void CustomUpdate(Vector2 moveDir)
    {
        movement?.Move(moveDir);
        weapon?.Fire();
        if(Input.GetKeyDown(KeyCode.Space))
            weapon?.LunchBomb();
    }

    public void StartGame()
    {
        movement?.SetEnable(true);
        weapon?.SetEnable(true);
    }

    public void StopGame()
    {
        movement?.SetEnable(false);
        weapon?.SetEnable(false);
    }

    private void Update()
    {
        weapon.Fire();
    }
}