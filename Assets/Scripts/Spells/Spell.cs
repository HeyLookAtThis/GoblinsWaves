using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] private float manaCost;
    [SerializeField] private int levels;
    [SerializeField] private int upgradeCost;
    [SerializeField] private Sprite icon;

    protected Dictionary<int, string> levelsDescriptions = new Dictionary<int, string>();

    protected int currentLevel;
    protected float currentManaCost;

    private float _flyingSpeed = 50;

    public Sprite Icon => icon;

    public int UpgradeCost => upgradeCost;

    public int Levels => levels;

    public int CurrentLevel => currentLevel;

    public float ManaCost => currentManaCost;

    private void Awake()
    {
        currentManaCost = manaCost;
    }

    public abstract void SetLevel(int level);

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
