using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpellUpgrade : MonoBehaviour
{
    [SerializeField] private bool _isUpgraded;

    private Spell _spell;
    private Button _button;
    private SpellInfoView _spellInfoView;
    private int _spellPrice;
    private string _description;
    private Player _player;

    public string Description => _description;

    public int SpellPrice => _spellPrice;

    public bool IsUpgraded => _isUpgraded;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowSpellInfo);
        _spellInfoView.OnBuyButtonClicked += Upgrade;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowSpellInfo);
        _spellInfoView.OnBuyButtonClicked -= Upgrade;
    }

    public void Initialize(Spell spell, int coefficient, string description, SpellInfoView spellInfo, Player player)
    {
        _spell = spell;
        _spellPrice = _spell.UpgradeCost * coefficient;
        _description = description;
        _button.image.sprite = _spell.Icon;
        _spellInfoView = spellInfo;
        _player = player;
    }

    public void SetButtonInteractable(bool isInteractable)
    {
        _button.interactable = isInteractable;
    }

    public void Upgrade()
    {
        _isUpgraded = true;
        _player.UpgradeSpell(_spell, _spellPrice);
    }

    private void ShowSpellInfo()
    {
        _spellInfoView.gameObject.SetActive(true);
        _spellInfoView.SetInfo(this);
    }
}
