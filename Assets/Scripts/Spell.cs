using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _flyingSpeed;

    public void Fly(Transform enemy)
    {
        StartCoroutine(FlyingTowardsEnemy(enemy));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator FlyingTowardsEnemy(Transform enemy)
    {
        float second = 0.01f;

        var waitTime = new WaitForSeconds(second);

        while (true)
        {
            if(enemy == null || transform.position == enemy.position)
            {
                Destroy(gameObject);
                yield break;
            }

            if (transform.position != enemy.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemy.position, _flyingSpeed * second);
                yield return waitTime;
            }
        }
    }
}
