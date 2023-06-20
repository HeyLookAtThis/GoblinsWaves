using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    
    private float _capacity;

    private List<Enemy> _pool = new List<Enemy>();

    protected void SetCapacity(float capacity)
    {
        _capacity = capacity;
    }

    protected void Initialize(Enemy prefab)
    {
        _pool.Clear();
        TryClearContainer();
        Debug.Log("Clear");

        for (int i = 0; i < _capacity; i++)
        {
            Enemy spawenedObject = Instantiate(prefab, _container.transform);
            spawenedObject.gameObject.SetActive(false);
            _pool.Add(spawenedObject);
        }
    }

    protected bool TryGetObject(out Enemy result)
    {
        result = _pool.FirstOrDefault(notActiveObject => notActiveObject.gameObject.activeSelf == false);
        return result != null;
    }

    private void TryClearContainer()
    {
        if (_container.GetComponentsInChildren<Enemy>() != null)
        {
            Enemy[] enemies = _container.GetComponentsInChildren<Enemy>();

            foreach(Enemy enemy in enemies)
                enemy.Destroy();
        }
    }
}
