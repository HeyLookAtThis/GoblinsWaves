using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChainController : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private DescriptionPanel _windowBuySpells;
    [SerializeField] private List<Spell> _spells;
    [SerializeField] private UpgradesChain _spellChain;
    [SerializeField] private Player _player;

    private void Awake()
    {
        CreateChains();
    }

    private void OnEnable()
    {
        _windowBuySpells.OnTransferingUpgradeButton += UpgradeSpell;
    }

    private void OnDisable()
    {
        _windowBuySpells.OnTransferingUpgradeButton -= UpgradeSpell;
    }

    private void CreateChains()
    {
        foreach (Spell spell in _spells)
            Instantiate(_spellChain, _container.transform).Create(spell, _windowBuySpells, _player);
    }

    private void UpgradeSpell(UpgradeButton upgrade)
    {
        _player.TryUpgradeSpell(upgrade);
    }
}
