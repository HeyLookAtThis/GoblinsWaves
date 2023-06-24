using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected float manaCost;

    [SerializeField] private Sprite _icon;
    [SerializeField] private float _flyingSpeed;

    protected int upgradeCost;
    protected int levels;
    protected Dictionary<int, string> levelsDescriptions;

    public Sprite Icon => _icon;

    public int UpgradeCost => upgradeCost;

    public int Levels => levels;

    public float ManaCost => manaCost;

    public abstract void Upgrade();

    public abstract void InitializeLevelsDescriptions();

    public abstract string ShowLevelDescription(int level);

    public void Fly(Transform enemy)
    {
        StartCoroutine(FlyingTowardsEnemy(enemy));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.Attacked == false)
                enemy.Die();

            enemy.SetAttacked(true);
            Destroy(gameObject);
        }
    }

    private IEnumerator FlyingTowardsEnemy(Transform target)
    {
        float second = 0.01f;

        var waitTime = new WaitForSeconds(second);

        while (true)
        {
            if(transform.position == target.position)
            {
                Destroy(gameObject);
                yield break;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _flyingSpeed * second);
                yield return waitTime;
            }
        }
    }
}
