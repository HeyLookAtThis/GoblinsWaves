using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
public class DeathAttack : MonoBehaviour
{
    private const string Attack = (nameof(Attack));

    [SerializeField] private float _attackDistance;
    [SerializeField] private float _speed;
    [SerializeField] private float _disappearanceDistance;

    private Enemy _enemy;
    private Animator _animator;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _enemy.Target.transform.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _enemy.Target.transform.position) <= _attackDistance)
            _animator.Play(Attack);

        if (Vector3.Distance(transform.position, _enemy.Target.transform.position) <= _disappearanceDistance)
        {
            _enemy.TryAttack();
            gameObject.SetActive(false);
        }
    }
}
