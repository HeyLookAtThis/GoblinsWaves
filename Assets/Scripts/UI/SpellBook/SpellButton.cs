using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpellButton : MonoBehaviour
{
    [SerializeField] private bool _isBought;

    private Spell _spell;
    private Button _button;
    private int _spellPrice;
    private string _description;

    public string Description => _description;

    public int SpellPrice => _spellPrice;

    public bool IsBought => _isBought;

    private void Start()
    {
        _button = GetComponent<Button>();

        //_button.image.sprite = _spell.Icon;
    }

    public void Initialize(Spell spell, int coefficient, string description)
    {
        _spell = spell;
        _spellPrice = _spell.UpgradeCost * coefficient;
        _description = description;
    }

    public void SetButtonInteractable( bool isInteractable)
    {
        _button.interactable = isInteractable;
    }

    public void Buy()
    {
        _isBought = true;
        _spell.Upgrade();
    }
}
