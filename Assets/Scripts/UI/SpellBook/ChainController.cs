using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChainController : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private WindowBuySpells _spellInfoView;
    [SerializeField] private List<Spell> _spells;
    [SerializeField] private SpellsUpgradesChain _spellChain;
    [SerializeField] private Player _player;

    private void Awake()
    {
        CreateChains();
    }

    private void OnEnable()
    {
        _spellInfoView.OnTransferingSpellAndPrice += UpgradeSpell;
    }

    private void OnDisable()
    {
        _spellInfoView.OnTransferingSpellAndPrice -= UpgradeSpell;
    }

    private void CreateChains()
    {
        foreach (Spell spell in _spells)
        {
            SpellsUpgradesChain spellChain = Instantiate(_spellChain, _container.transform);
            spellChain.CreateButtons(spell, _spellInfoView, _player);
        }
    }

    private void UpgradeSpell(Spell spell, int upgradeCost)
    {
        _player.TryUpgradeSpell(spell, upgradeCost);
    }
}
