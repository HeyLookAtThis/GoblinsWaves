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
        _spawner.OnAllEnemiesDied += SetActive;
        _nextWaveButton.onClick.AddListener(SetNextWave);
    }

    private void OnDisable()
    {
        _spawner.OnAllEnemiesDied -= SetActive;
        _nextWaveButton.onClick.RemoveListener(SetNextWave);
    }

    private void SetActive()
    {
        _nextWaveButton.gameObject.SetActive(true);
    }

    private void SetNextWave()
    {
        _spawner.SetNextWave();
        _nextWaveButton.gameObject.SetActive(false);
    }
}
