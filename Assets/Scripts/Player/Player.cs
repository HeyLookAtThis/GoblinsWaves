using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _mana;
    [SerializeField] private List<Spell> _spells;
    [SerializeField] private Transform _spellHand;

    private int _startSpellLevel = 1;
    private Coroutine _deactivator;
    private Spell _currentSpell;

    public float Health => _health;

    public float Mana => _mana;

    public int Rewards { get; private set; }

    private void Awake()
    {
        _currentSpell = _spells[0];
        _currentSpell.SetLevel(_startSpellLevel);

        Rewards = 200;
    }

    private UnityAction _attacking;
    private UnityAction _died;

    private UnityAction<float> _changedHealth;
    private UnityAction<float> _changedMana;
    private UnityAction<int> _changedRewards;
    private UnityAction<bool> _trySpellUpgrade;

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

    public event UnityAction<bool> OnTrySpellUpgrade
    {
        add => _trySpellUpgrade += value;
        remove => _trySpellUpgrade -= value;
    }

    public int GetSpellLevel(Spell spell)
    {
        Debug.Log(spell);

        if (_spells.Contains(spell))
        {
            Debug.Log(_spells.Find(playerSpell => playerSpell == spell).CurrentLevel);

            return _spells.Find(playerSpell => playerSpell == spell).CurrentLevel;
        }
        else
            Debug.Log("no");

        return _startSpellLevel;
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

        if (_mana >= _currentSpell.ManaCost)
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

    public bool TryUpgradeSpell(UpgradeButton upgrade)
    {
        if (Rewards >= upgrade.Price)
        {
            UpgradeSpell(upgrade.Spell, upgrade.Price, upgrade.Level);
            _trySpellUpgrade?.Invoke(true);
            return true;
        }

        _trySpellUpgrade?.Invoke(false);
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

        Debug.Log(_spells[0].CurrentLevel);


        Rewards -= cost;
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
        Spell spell = Instantiate(_currentSpell, _spellHand.position, Quaternion.identity);
        Debug.Log($"Instantiate {spell.CurrentLevel}");

        _mana -= spell.ManaCost;
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
