using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpellUpgradeButton : MonoBehaviour
{
    [SerializeField] private bool _isUpgraded;
    [SerializeField] private Player _player;

    private Spell _spell;
    private Button _button;
    private WindowBuySpells _windowBuySpells;
    private int _spellPrice;
    private string _description;
    private int _spellButtonLevel;

    public string Description => _description;

    public int SpellPrice => _spellPrice;

    public bool IsUpgraded => _isUpgraded;

    public Spell Spell => _spell;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowSpellInfo);
        _player.OnTrySpellUpgrade += TryUpgrade;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowSpellInfo);
        _player.OnTrySpellUpgrade -= TryUpgrade;
    }

    public void Initialize(Spell spell, int spellLevel, string description, WindowBuySpells spellInfo)
    {
        _spell = spell;
        _spellPrice = _spell.UpgradeCost * spellLevel;
        _spellButtonLevel = spellLevel;
        _description = description;
        _button.image.sprite = _spell.Icon;
        _windowBuySpells = spellInfo;
    }

    public void SetButtonInteractable(bool isInteractable)
    {
        _button.interactable = isInteractable;
    }

    public void SetUpgrade(int? spellLevel)
    {
        if (_spellButtonLevel == spellLevel)
            _isUpgraded = true;

        ShowSpellInfo();
    }

    public void TryUpgrade(bool isSuccsses)
    {
        _isUpgraded = isSuccsses;
        ShowSpellInfo();
    }

    private void ShowSpellInfo()
    {
        _windowBuySpells.gameObject.SetActive(true);
        _windowBuySpells.SetInfo(this);
    }
}
