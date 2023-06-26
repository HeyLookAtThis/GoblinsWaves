using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WindowBuySpells : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;

    private Spell _spell;
    private int _price;

    private UnityAction<Spell, int> _transferingSpellAndPrice;

    public event UnityAction<Spell, int> OnTransferingSpellAndPrice
    {
        add => _transferingSpellAndPrice += value;
        remove => _transferingSpellAndPrice -= value;
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(InvokingActions);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(InvokingActions);
    }

    public void SetInfo(SpellUpgradeButton button)
    {
        _description.text = button.Description;
        _price = button.SpellPrice;
        _priceText.text = button.SpellPrice.ToString();
        _spell = button.Spell;

        SetBuyButtonVisible(button);
    }

    private void SetBuyButtonVisible(SpellUpgradeButton spell)
    {
        if (spell.IsUpgraded)
            SetBuyButtonVisible(false);
        else
            SetBuyButtonVisible(true);
    }

    private void SetBuyButtonVisible(bool isVisible)
    {
        _buyButton.gameObject.SetActive(isVisible);
        _priceText.gameObject.SetActive(isVisible);
    }

    private void InvokingActions()
    {
        _transferingSpellAndPrice?.Invoke(_spell, _price);
    }
}
