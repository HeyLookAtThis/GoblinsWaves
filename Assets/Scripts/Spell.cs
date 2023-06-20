using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private float _manaCost;
    [SerializeField] private float _flyingSpeed;

    public float ManaCost => _manaCost;

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
