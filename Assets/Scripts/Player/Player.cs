using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _mana;
    [SerializeField] private List<Spell> _spells;
    [SerializeField] private Transform _spellHand;

    private int _startSpellLevel = 1;
    private Coroutine _deactivator;
    private Spell _currentSpell;

    private float _currentMana;
    private float _currentHealth;

    public float Health => _currentHealth;

    public float Mana => _currentMana;

    public int Rewards { get; private set; }

    private UnityAction _attacking;
    private UnityAction _died;

    private UnityAction<float> _changedHealth;
    private UnityAction<float> _changedMana;
    private UnityAction<int> _changedRewards;

    public event UnityAction Attacking
    {
        add => _attacking += value;
        remove => _attacking -= value;
    }

    public event UnityAction Died
    {
        add => _died += value;
        remove => _died -= value;
    }

    public event UnityAction<float> ChangedHealth
    {
        add => _changedHealth += value;
        remove => _changedHealth -= value;
    }

    public event UnityAction<float> ChangedMana
    {
        add => _changedMana += value;
        remove => _changedMana -= value;
    }

    public event UnityAction<int> ChangedRewards
    {
        add => _changedRewards += value;
        remove => _changedRewards -= value;
    }

    private void Awake()
    {
        _currentMana = _mana;
        _currentHealth = _health;
    }

    private void Start()
    {
        _currentSpell = _spells[0];
        _currentSpell.SetLevel(_startSpellLevel);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _changedHealth?.Invoke(Health);

        if (_currentHealth <= 0)
        {
            _died?.Invoke();

            if (_deactivator == null)
                _deactivator = StartCoroutine(Disabler());
        }
    }

    public void TryAttack(Transform enemy)
    {
        RotateToEnemy(enemy);

        if (_currentMana >= _currentSpell.ManaCost)
        {
            _attacking?.Invoke();
            CastSpell(enemy);
        }
    }

    public void AddReward(int reward)
    {
        Rewards += reward;
        _changedRewards?.Invoke(Rewards);
    }

    public bool TryUpgradeSpell(UpgradeButton upgrade)
    {
        if (Rewards >= upgrade.Price)
        {
            UpgradeSpell(upgrade.Spell, upgrade.Price, upgrade.Level);
            upgrade.SetUpgrade(true);
            return true;
        }

        upgrade.SetUpgrade(false);
        return false;
    }

    public bool CheckSpell(Spell spell, int level)
    {
        var foundSpell = _spells.FirstOrDefault(desiredSpell => desiredSpell == spell && desiredSpell.CurrentLevel == level);
        
        if (foundSpell != null)
            return true;

        return false;
    }

    private void UpgradeSpell(Spell spell, int cost, int level)
    {
        var upgradedSpell = _spells.FirstOrDefault(upgradedSpell => upgradedSpell == spell);

        if (upgradedSpell != null)
        {
            var index = _spells.IndexOf(upgradedSpell);
            _spells[index].SetLevel(level);
        }
        else
        {
            _spells.Add(spell);
        }

        Rewards -= cost;
        _changedRewards?.Invoke(Rewards);
    }

    private void RotateToEnemy(Transform target)
    {
        Vector3 targetDirection = target.position - transform.position;
        targetDirection.y = 0;
        transform.forward = targetDirection;
    }

    private void CastSpell(Transform target)
    {
        Spell spell = Instantiate(_currentSpell, _spellHand.position, Quaternion.identity);
        spell.SetLevel(_currentSpell.CurrentLevel);

        _currentMana -= spell.ManaCost;
        _changedMana?.Invoke(Mana);

        if (_currentMana >= _mana)
            _currentMana = _mana;

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
