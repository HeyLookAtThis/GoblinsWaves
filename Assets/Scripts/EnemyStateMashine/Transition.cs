using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Player Target { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; private set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Initialize(Player target)
    {
        Target = target;
    }
}
