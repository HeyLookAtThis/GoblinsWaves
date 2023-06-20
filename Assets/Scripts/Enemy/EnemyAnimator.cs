using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(MoveState))]
[RequireComponent(typeof(PlayerDieState))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    private MoveState _moveState;
    private PlayerDieState _playerDieState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _moveState = GetComponent<MoveState>();
        _playerDieState = GetComponent<PlayerDieState>();
    }

    private void OnEnable()
    {
        _moveState.OnWalking += PlayWalk;
        _enemy.OnAttacking += PlayAttack;
        _enemy.OnDied += PlayDying;
        _playerDieState.OnCelebrating += PlayCelebrate;
    }

    private void OnDisable()
    {
        _moveState.OnWalking -= PlayWalk;
        _enemy.OnAttacking -= PlayAttack;
        _enemy.OnDied -= PlayDying;
        _playerDieState.OnCelebrating -= PlayCelebrate;
    }

    private void PlayWalk(float speed)
    {
        _animator.SetFloat(ACSkeleton.Params.Speed, speed);
    }

    private void PlayAttack()
    {
        _animator.Play(ACSkeleton.State.Attack);
    }

    private void PlayDying()
    {
        _animator.Play(ACSkeleton.State.Death);
    }

    private void PlayCelebrate()
    {
        string[] animations = { ACSkeleton.State.CelebrateFirst, ACSkeleton.State.CelebrateSecond, ACSkeleton.State.CelebrateThird };

        _animator.Play(animations[Random.Range(0, animations.Length)]);
    }
}
