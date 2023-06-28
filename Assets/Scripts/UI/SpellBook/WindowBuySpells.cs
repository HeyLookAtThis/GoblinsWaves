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

    private UpgradeButton _button;

    private UnityAction<UpgradeButton> _transferingUpgradeButton;

    public event UnityAction<UpgradeButton> OnTransferingUpgradeButton
    {
        add => _transferingUpgradeButton += value;
        remove => _transferingUpgradeButton -= value;
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(InvokeAction);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(InvokeAction);
    }

    public void SetInfo(UpgradeButton button)
    {
        _description.text = button.Description;
        _priceText.text = button.Price.ToString();
        _button = button;

        SetBuyButtonVisible(button);
    }

    private void SetBuyButtonVisible(UpgradeButton button)
    {
        if (button.IsUpgraded)
            SetBuyButtonVisible(false);
        else
            SetBuyButtonVisible(true);
    }

    private void SetBuyButtonVisible(bool isVisible)
    {
        _buyButton.gameObject.SetActive(isVisible);
        _priceText.gameObject.SetActive(isVisible);
    }

    private void InvokeAction()
    {
        _transferingUpgradeButton?.Invoke(_button);
    }
}
