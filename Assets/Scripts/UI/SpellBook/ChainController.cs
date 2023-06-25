using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChainController : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private SpellInfoView _spellInfoView;
    [SerializeField] private List<Spell> _spells;
    [SerializeField] private SpellsUpgradesChain _spellChain;
    [SerializeField] private Player _player;

    private void Start()
    {
        CreateChains();
    }

    private void CreateChains()
    {
        foreach (Spell spell in _spells)
        {
            SpellsUpgradesChain spellChain = Instantiate(_spellChain, _container.transform);
            spellChain.CreateButtons(spell, _spellInfoView, _player);
        }
    }
}
