using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private int _experience;
    [SerializeField] private Transform _spellTarget;

    public float Health => _health;

    public Player Target { get; private set; }

    private UnityAction _died;
    private UnityAction _attacking;
    private UnityAction _tookDamage;

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    public event UnityAction OnDied
    {
        add => _died += value;
        remove => _died -= value;
    }

    public event UnityAction OnAttacking
    {
        add => _attacking += value;
        remove => _attacking -= value;
    }

    public event UnityAction OnTookDamage
    {
        add => _tookDamage += value;
        remove => _tookDamage -= value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Target.Attack(_spellTarget);
    }

    public void InitializeTarget(Player target)
    {
        Target = target;
    }

    public void Attack()
    {
        _attacking?.Invoke();
        Target.TakeDamage(_damage);
    }

    public void TakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            _tookDamage?.Invoke();
        }
        
        if (_health <= 0)
        {
            _died?.Invoke();
            Target.GainExperience(_experience);
            StartCoroutine(Deactivator());
        }
    }

    private void LookAtTarget()
    {
        Vector3 targetDirection = Target.transform.position - transform.position;
        transform.forward = targetDirection;
    }

    private IEnumerator Deactivator()
    {
        float second = 1.0f;
        var waitTime = new WaitForSeconds(second);
        bool isSkeppedTime = false;

        while (isSkeppedTime == false)
        {
            isSkeppedTime = true;
            yield return waitTime;
        }

        if (isSkeppedTime)
        {
            gameObject.SetActive(false);
            yield break;
        }
    }
}
