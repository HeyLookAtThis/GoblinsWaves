using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Enemy))]
public class AttackState : State
{
    [SerializeField] private float _delay;

    private float _lastAttackTime;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _lastAttackTime = _delay;        
    }

    private void Update()
    {
        _lastAttackTime += Time.deltaTime;

        if (_lastAttackTime >= _delay)
        {
            _enemy.TryAttack();
            _lastAttackTime = 0;
        }
    }
}
