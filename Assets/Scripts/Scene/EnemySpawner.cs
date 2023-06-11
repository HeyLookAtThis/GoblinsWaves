using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Player _target;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;

    private int _spawnedEnemiesCount;
    private float _timeAfterLastSpawn;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawnedEnemiesCount++;
            _timeAfterLastSpawn = 0;
        }

        if(_spawnedEnemiesCount == _waves.Count)
            _currentWave = null;
    }

    private void InstantiateEnemy()
    {
        Vector3 position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;

        Enemy enemy = Instantiate(_currentWave.Enemy, position, Quaternion.identity);
        enemy.InitializeTarget(_target);
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }
}

[System.Serializable]
public class Wave
{
    public Enemy Enemy;
    public float Delay;
    public int Count;
}
