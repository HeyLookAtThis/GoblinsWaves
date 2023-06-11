using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    public Player Target { get; private set; }

    public void Enter(Player target)
    {
        if(enabled == false)
        {
            enabled = true;
            Target = target;

            foreach(var transition in _transitions)
            {
                enabled = true;
                transition.Initialize(target);
            }
        }
    }

    public State GetNext()
    {
        foreach(var transition in _transitions)
            if(transition.NeedTransit)
                return transition.TargetState;

        return null;
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }
}
