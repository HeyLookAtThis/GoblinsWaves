using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : Bar
{
    [SerializeField] private EnemySpawner _spawner;

    private float _step;
    private float _targetValue;

    private void Start()
    {
        Speed = 0.002f;
    }

    private void OnEnable()
    {
        _spawner.OnSetWave += SetDefaultState;
        Player.OnAttacking += SetNextValue;
    }

    private void OnDisable()
    {
        _spawner.OnSetWave -= SetDefaultState;
        Player.OnAttacking -= SetNextValue;
    }

    private void SetDefaultState(int enemyCount)
    {
        Slider.value = 0;
        _targetValue = 0;
        _step = Slider.maxValue / enemyCount;
    }

    private void SetNextValue()
    {
        _targetValue += _step;

        BeginChangeValue(_targetValue);
    }
}
