using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : EnemyPool
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Player _target;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;

    private int _spawnedEnemiesCount;
    private float _timeAfterLastSpawn;

    private List<int> _previousSpawnPoints = new List<int>();

    private void Start()
    {
        SetWave(_currentWaveNumber);
        Initialize(_currentWave.Enemy);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.DelayBetwenSpawn)
        {
            if (TryGetObject(out Enemy enemy))
            {
                SetEnemy(enemy, GetNextPosition());
                _spawnedEnemiesCount++;
                _timeAfterLastSpawn = 0;
            }
        }

        if (_spawnedEnemiesCount == _currentWave.TotalAmountEnemies)
            _currentWave = null;
    }

    private Vector3 GetNextPosition()
    {
        int index = Random.Range(0, _spawnPoints.Length);
        bool isContinue = true;

        if(_previousSpawnPoints.Count == _spawnPoints.Length)
        {
            _previousSpawnPoints.Clear();
            isContinue = false;
        }

        while(isContinue)
        {
            if (_previousSpawnPoints.Contains(index))
            {
                index = Random.Range(0, _spawnPoints.Length);
            }
            else
            {
                isContinue = false;
                _previousSpawnPoints.Add(index);
            }
        }

        return _spawnPoints[index].position;
    }

    private void SetEnemy(Enemy enemy, Vector3 position)
    {
        enemy.gameObject.SetActive(true);
        enemy.InitializeTarget(_target);
        enemy.transform.position = position;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        SetCapacity(_currentWave.AmountEnemiesOnScene);
    }
}

[System.Serializable]
public class Wave
{
    public Enemy Enemy;
    public float DelayBetwenSpawn;
    public int TotalAmountEnemies;
    public int AmountEnemiesOnScene;
}
