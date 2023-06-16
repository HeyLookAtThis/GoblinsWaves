using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(MoveState))]
[RequireComponent(typeof(AttackState))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    private MoveState _moveState;
    private AttackState _attackState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _moveState = GetComponent<MoveState>();
    }

    private void OnEnable()
    {
        _moveState.OnWalking += PlayWalk;
        _enemy.OnAttacking += PlayAttack;
        _enemy.OnTookDamage += PlayTakeDamage;
        _enemy.OnDied += PlayDying;
    }

    private void OnDisable()
    {
        _moveState.OnWalking -= PlayWalk;
        _enemy.OnAttacking -= PlayAttack;
        _enemy.OnTookDamage -= PlayTakeDamage;
        _enemy.OnDied -= PlayDying;
    }

    private void PlayWalk(float speed)
    {
        _animator.SetFloat(ACSkeleton.Params.Speed, speed);
    }

    private void PlayAttack()
    {
        _animator.Play(ACSkeleton.State.Attack);
    }

    private void PlayTakeDamage()
    {
        _animator.Play(ACSkeleton.State.TakeDamage);
    }

    private void PlayDying()
    {
        _animator.Play(ACSkeleton.State.Death);
    }
}
