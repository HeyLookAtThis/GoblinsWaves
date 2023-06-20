using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _mana;
    [SerializeField] private Spell _spell;
    [SerializeField] private Transform _spellHand;

    private Coroutine _deactivator;

    public float Health => _health;

    public float Mana => _mana;

    public int Rewards { get; private set; } = 0;

    private UnityAction _attacking;
    private UnityAction _died;

    private UnityAction<float> _changedHealth;
    private UnityAction<float> _changedMana;
    private UnityAction<int> _changedRewards;

    public event UnityAction OnAttacking
    {
        add => _attacking += value;
        remove => _attacking -= value;
    }

    public event UnityAction OnDied
    {
        add => _died += value;
        remove => _died -= value;
    }

    public event UnityAction<float> OnChangedHealth
    {
        add => _changedHealth += value;
        remove => _changedHealth -= value;
    }

    public event UnityAction<float> OnChangedMana
    {
        add => _changedMana += value;
        remove => _changedMana -= value;
    }

    public event UnityAction<int> OnChangedRewards
    {
        add => _changedRewards += value;
        remove => _changedRewards -= value;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _changedHealth?.Invoke(Health);

        if (_health <= 0)
        {
            _died?.Invoke();

            if (_deactivator == null)
                _deactivator = StartCoroutine(Disabler());
        }
    }

    public void TryAttack(Transform enemy)
    {
        RotateToEnemy(enemy);

        if (_mana >= _spell.ManaCost)
        {
            _attacking?.Invoke();
            ShootSpell(enemy);
        }
    }

    public void AddReward(int reward)
    {
        Rewards += reward;
        _changedRewards?.Invoke(Rewards);
    }

    private void RotateToEnemy(Transform target)
    {
        Vector3 targetDirection = target.position - transform.position;
        targetDirection.y = 0;
        transform.forward = targetDirection;
    }

    private void ShootSpell(Transform target)
    {
        Spell spell = Instantiate(_spell, _spellHand.position, Quaternion.identity);

        _mana-= spell.ManaCost;
        _changedMana?.Invoke(Mana);

        spell.Fly(target);
    }

    private IEnumerator Disabler()
    {
        float second = 2.0f;
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
