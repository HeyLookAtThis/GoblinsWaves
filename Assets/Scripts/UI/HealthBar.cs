using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Bar
{
    private void Start()
    {
        Slider.maxValue = Player.Health;
        Slider.value = Player.Health;
    }

    private void OnEnable()
    {
        Player.ChangedHealth += OnBeginChangeValue;
    }

    private void OnDisable()
    {
        Player.ChangedHealth -= OnBeginChangeValue;
    }
}
