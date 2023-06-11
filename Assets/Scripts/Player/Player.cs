using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Arrow _arrow;
    [SerializeField] private Transform _handForArrow;
    [SerializeField] private float _shootSpeed;

    public float Experience { get; private set; } = 0;

    public int Level { get; private set; } = 1;

    private UnityAction _attacking;
    private UnityAction _tookDamage;
    private UnityAction _died;


    public event UnityAction OnAttacking
    {
        add => _attacking += value;
        remove => _attacking -= value;
    }

    public event UnityAction OnTookDamage
    {
        add => _tookDamage += value;
        remove => _tookDamage -= value;
    }

    public event UnityAction OnDied
    {
        add => _died += value;
        remove => _died -= value;
    }

    public void TakeDamage(float damage)
    {
        _health-=damage;
        _tookDamage?.Invoke();

        if (_health <= 0)
            _died?.Invoke();
    }

    public void Attack(Transform enemy)
    {
        _attacking?.Invoke();
        RotateToEnemy(enemy);
        ShootArrow(enemy);
    }

    public void GainExperience(float experience)
    {
        Experience += experience;
    }

    private void RotateToEnemy(Transform target)
    {
        Vector3 targetDirection = target.position - transform.position;
        Vector3 foreward = transform.forward;
        float angle = Vector3.SignedAngle(targetDirection, foreward, Vector3.up);
        angle *= -1;
        transform.Rotate(Vector3.up, angle, Space.World);
        transform.rotation = Quaternion.identity;
    }

    private void ShootArrow(Transform enemy)
    {
        Arrow arrow = Instantiate(_arrow, _handForArrow.position, Quaternion.identity);
        arrow.Fly(_shootSpeed, enemy);
    }
}
