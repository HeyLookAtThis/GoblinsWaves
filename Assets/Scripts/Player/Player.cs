using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private GameObject _handForWeapon;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Arrow _arrow;
    [SerializeField] private GameObject _handForArrow;

    private UnityAction _attacking;

    public event UnityAction OnAttacking
    {
        add => _attacking += value;
        remove => _attacking -= value;
    }

    public void Attack(Transform enemy)
    {
        _attacking?.Invoke();
        SetRotation(enemy);
        _weapon.Attack(enemy, _arrow, _handForArrow.transform);
    }

    private void SetRotation(Transform target)
    {
        Vector3 targetDirection = target.position - transform.position;
        Vector3 foreward = transform.forward;
        float angle = Vector3.Angle(targetDirection, foreward);

        Debug.DrawRay(transform.position, foreward * 10, Color.yellow);
        Debug.DrawRay(transform.position, target.position - transform.position * 10, Color.red);


        //transform.Rotate(0, angle, 0);
        Debug.Log(angle);
    }
}
