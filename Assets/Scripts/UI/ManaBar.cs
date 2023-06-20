using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ManaBar : Bar
{
    private void Start()
    {
        Slider.maxValue = Player.Mana;
        Slider.value = Player.Mana;
    }

    private void OnEnable()
    {
        Player.OnChangedMana += BeginChangeValue;
    }

    private void OnDisable()
    {
        Player.OnChangedMana -= BeginChangeValue;
    }
}
