using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : Bar
{
    private void Start()
    {
        Slider.maxValue = Player.Mana;
        Slider.value = Player.Mana;
    }

    private void OnEnable()
    {
        Player.ChangedMana += OnBeginChangeValue;
    }

    private void OnDisable()
    {
        Player.ChangedMana -= OnBeginChangeValue;
    }
}
