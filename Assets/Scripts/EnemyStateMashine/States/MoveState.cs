using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Target.transform.position * _speed * Time.deltaTime);
        transform.LookAt(Target.transform.position);
    }
}
