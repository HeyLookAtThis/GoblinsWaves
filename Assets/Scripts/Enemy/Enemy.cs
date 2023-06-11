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
    
    public Player Target { get; private set; }

    private UnityAction _died;
    private UnityAction _attacking;
    private UnityAction _tookDamage;

    public event UnityAction OnDied
    {
        add => _died += value;
        remove => _died -= value;
    }

    public event UnityAction OnTookDamage
    {
        add => _tookDamage += value;
        remove => _tookDamage -= value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_health > 0)
            Target.Attack(transform);
    }

    public void InitializeTarget(Player target)
    {
        Target = target;
    }

    public void Attack()
    {
        Target.TakeDamage(_damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Arrow>(out Arrow arrow))
            TakeDamage(arrow.Damage);
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        _tookDamage?.Invoke();

        if (_health <= 0)
        {
            _died?.Invoke();
            Target.GainExperience(_experience);
            StartCoroutine(Destroyer());
        }
    }

    private IEnumerator Destroyer()
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
            Destroy(gameObject);
            yield break;
        }
    }
}
