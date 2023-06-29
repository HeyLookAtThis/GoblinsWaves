using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Player _player;

    private bool _isUpgraded;
    private Spell _spell;
    private Button _button;
    private DescriptionPanel _windowBuySpells;
    private int _price;
    private string _description;
    private int _level;

    private UnityAction<UpgradeButton> _upgraded;

    public string Description => _description;

    public int Price => _price;

    public bool IsUpgraded => _isUpgraded;

    public int Level => _level;

    public Spell Spell => _spell;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowSpellInfo);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowSpellInfo);
    }

    public event UnityAction<UpgradeButton> OnUpgraded
    {
        add => _upgraded += value;
        remove => _upgraded -= value;
    }

    public void Initialize(Spell spell, int upgradeLevel, string description, DescriptionPanel spellInfo)
    {
        _spell = spell;
        _price = _spell.UpgradeCost * upgradeLevel;
        _level = upgradeLevel;
        _description = description;
        _button.image.sprite = _spell.Icon;
        _windowBuySpells = spellInfo;
    }

    public void SetButtonInteractable(bool isInteractable)
    {
        _button.interactable = isInteractable;
    }

    public void SetUpgrade(bool upgraded)
    {
        _isUpgraded = upgraded;
        _upgraded?.Invoke(this);

        SetButtonInteractable(upgraded);
        ShowSpellInfo();
    }

    private void ShowSpellInfo()
    {
        _windowBuySpells.SetInfo(this);
    }
}