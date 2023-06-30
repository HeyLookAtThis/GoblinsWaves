using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Button _nextWaveButton;

    private void OnEnable()
    {
        _spawner.AllEnemiesDied += OnSetActive;
        _nextWaveButton.onClick.AddListener(OnSetNextWave);
    }

    private void OnDisable()
    {
        _spawner.AllEnemiesDied -= OnSetActive;
        _nextWaveButton.onClick.RemoveListener(OnSetNextWave);
    }

    private void OnSetActive()
    {
        _nextWaveButton.gameObject.SetActive(true);
    }

    private void OnSetNextWave()
    {
        _spawner.SetNextWave();
        _nextWaveButton.gameObject.SetActive(false);
    }
}
