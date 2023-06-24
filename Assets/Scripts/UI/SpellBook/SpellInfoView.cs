using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buyButton;

    public void SetInfo(SpellButton button)
    {
        _description.text = button.Description;
        _price.text = button.SpellPrice.ToString();

        SetBuyButtonVisible(button);
    }

    private void SetBuyButtonVisible(SpellButton spell)
    {
        if (spell.IsBought)
            SetBuyButtonVisible(false);
        else
            SetBuyButtonVisible(true);
    }

    private void SetBuyButtonVisible(bool isVisible)
    {
        _buyButton.gameObject.SetActive(isVisible);
        _price.gameObject.SetActive(isVisible);
    }
}
