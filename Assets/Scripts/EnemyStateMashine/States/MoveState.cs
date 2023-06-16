using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class MoveState : State
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private UnityAction<float> _walking;

    public event UnityAction<float> OnWalking
    {
        add => _walking += value;
        remove => _walking -= value;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = Target.transform.position - transform.position;
        _walking?.Invoke(_speed);

        _rigidbody.MovePosition(transform.position + direction.normalized * _speed * Time.deltaTime);        
    }

    private void OnDisable()
    {
        _walking?.Invoke(0);
    }
}
