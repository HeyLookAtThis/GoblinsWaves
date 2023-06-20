using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class PlayerDieState : State
{
    private UnityAction _celebrating;

    public event UnityAction OnCelebrating
    {
        add => _celebrating += value;
        remove => _celebrating -= value;
    }

    private void OnEnable()
    {
        _celebrating?.Invoke();
    }
}
