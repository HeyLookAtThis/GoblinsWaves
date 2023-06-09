using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private List<int> _previousSpawnPointsIndices = new List<int>();

    private UnityAction<int> _setWave;
    private UnityAction _allEnemiesSpawned;

    public event UnityAction AllEnemiesDied
    {
        add => _allEnemiesSpawned += value;
        remove => _allEnemiesSpawned -= value;
    }

    public event UnityAction<int> SetWave
    {
        add => _setWave += value;
        remove => _setWave -= value;
    }

    private void Start()
    {
        SetCurrentWave(_currentWaveNumber);
        Initialize(_currentWave.Enemy);
    }

    private void Update()
    {
        if (_currentWave == null)
        {
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.DelayBetwenSpawn && _spawnedEnemiesCount < _currentWave.TotalAmountEnemies)
        {
            if (TryGetObject(out Enemy enemy))
            {
                SetEnemy(enemy, GetNextPosition());
                _spawnedEnemiesCount++;
                _timeAfterLastSpawn = 0;
            }
        }

        if (_spawnedEnemiesCount == _currentWave.TotalAmountEnemies && WasAllEnemiesDied())
        {
            if (_waves.Count > _currentWaveNumber + 1)
                _allEnemiesSpawned?.Invoke();

            _currentWave = null;
            ClearPool();
        }
    }

    public void SetNextWave()
    {
        SetCurrentWave(++_currentWaveNumber);
        Initialize(_currentWave.Enemy);
        _spawnedEnemiesCount = 0;
    }

    private Vector3 GetNextPosition()
    {
        int index = Random.Range(0, _spawnPoints.Length);
        bool isContinue = true;

        if(_previousSpawnPointsIndices.Count == _spawnPoints.Length)
        {
            _previousSpawnPointsIndices.Clear();
            isContinue = false;
        }

        while(isContinue)
        {
            if (_previousSpawnPointsIndices.Contains(index))
            {
                index = Random.Range(0, _spawnPoints.Length);
            }
            else
            {
                isContinue = false;
                _previousSpawnPointsIndices.Add(index);
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

    private void SetCurrentWave(int index)
    {
        _currentWave = _waves[index];
        SetCapacity(_currentWave.AmountEnemiesOnScene);
        _setWave?.Invoke(_currentWave.TotalAmountEnemies);
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _delayBetwenSpawn;
    [SerializeField] private int _totalAmountEnemies;
    [SerializeField] private int _amountEnemiesOnScene;

    public Enemy Enemy => _enemy;

    public float DelayBetwenSpawn => _delayBetwenSpawn;

    public int TotalAmountEnemies => _totalAmountEnemies;

    public int AmountEnemiesOnScene => _amountEnemiesOnScene;
}
