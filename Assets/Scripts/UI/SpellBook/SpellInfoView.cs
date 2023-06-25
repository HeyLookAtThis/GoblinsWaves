using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpellInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buyButton;

    private UnityAction _buyButtonCliked;

    public event UnityAction OnBuyButtonClicked
    {
        add => _buyButtonCliked += value;
        remove => _buyButtonCliked -= value;
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(ClickBuyButton);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(ClickBuyButton);
    }

    public void SetInfo(SpellUpgrade button)
    {
        _description.text = button.Description;
        _price.text = button.SpellPrice.ToString();

        SetBuyButtonVisible(button);
    }

    private void SetBuyButtonVisible(SpellUpgrade spell)
    {
        if (spell.IsUpgraded)
            SetBuyButtonVisible(false);
        else
            SetBuyButtonVisible(true);
    }

    private void SetBuyButtonVisible(bool isVisible)
    {
        _buyButton.gameObject.SetActive(isVisible);
        _price.gameObject.SetActive(isVisible);
    }

    private void ClickBuyButton()
    {
        _buyButtonCliked?.Invoke();
    }
}
