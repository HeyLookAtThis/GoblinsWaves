using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _damage;
    [SerializeField] private int _reward;
    [SerializeField] private Transform _spellTarget;

    public bool Attacked { get; private set; }

    public Player Target { get; private set; }

    private UnityAction _died;
    private UnityAction _attacking;

    private void OnEnable()
    {
        SetAttacked(false);
    }    

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Attacked == false && Target.gameObject.activeSelf)
            Target.TryAttack(_spellTarget);
    }

    public void SetAttacked(bool wasAttacked)
    {
        Attacked = wasAttacked;
    }

    public void InitializeTarget(Player target)
    {
        Target = target;
    }

    public void TryAttack()
    {
        if (Target.Health > 0)
        {
            _attacking?.Invoke();
            Target.TakeDamage(_damage);
        }
    }

    public void Die()
    {
        _died?.Invoke();
        Target.AddReward(_reward);
        StartCoroutine(Deactivator());
    }

    public void Destroy()
    {
        Destroy(gameObject);
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
