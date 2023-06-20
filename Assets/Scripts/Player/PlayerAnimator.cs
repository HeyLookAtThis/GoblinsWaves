using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.OnAttacking += SetShotTrigger;
        _player.OnDied += PlayDeath;
    }

    private void OnDisable()
    {
        _player.OnAttacking -= SetShotTrigger;
        _player.OnDied -= PlayDeath;
    }

    private void SetShotTrigger()
    {
        _animator.SetTrigger(ACPlayer.Params.Shot);
    }

    private void PlayDeath()
    {
        _animator.Play(ACPlayer.State.Death);
    }
}
