using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DescriptionPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private AudioClip _buttonSound;

    private UpgradeButton _button;
    private AudioSource _audio;

    private UnityAction<UpgradeButton> _transferingUpgradeButton;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public event UnityAction<UpgradeButton> TransferingUpgradeButton
    {
        add => _transferingUpgradeButton += value;
        remove => _transferingUpgradeButton -= value;
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(TransferUpgradeButton);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(TransferUpgradeButton);
    }

    public void SetInfo(UpgradeButton button)
    {
        _description.text = button.Description;
        _priceText.text = button.Price.ToString();
        _button = button;

        SetBuyButtonAvailabel(button.IsAvailable);
        SetBuyButtonVisible(button);
    }

    private void SetBuyButtonVisible(UpgradeButton button)
    {
        if (button.IsUpgraded)
            SetBuyButtonVisible(false);
        else
            SetBuyButtonVisible(true);
    }

    private void SetBuyButtonAvailabel(bool iaAvailabel)
    {
        _buyButton.interactable = iaAvailabel;
    }

    private void SetBuyButtonVisible(bool isVisible)
    {
        _buyButton.gameObject.SetActive(isVisible);
        _priceText.gameObject.SetActive(isVisible);
    }

    private void TransferUpgradeButton()
    {
        _transferingUpgradeButton?.Invoke(_button);
        _audio.PlayOneShot(_buttonSound);
    }
}
