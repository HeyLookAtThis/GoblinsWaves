using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _damage;

    public float Damage => _damage;

    public void Fly(float forse, Transform enemy)
    {
        StartCoroutine(FlyingTowardsEnemy(forse, enemy));
    }

    private IEnumerator FlyingTowardsEnemy(float forse, Transform enemy)
    {
        float second = 0.01f;

        var waitTime = new WaitForSeconds(second);

        transform.up = -enemy.position;

        while (transform.position != enemy.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.position, forse * second);

            yield return waitTime;
        }

        if(transform.position == enemy.position)
        {
            Destroy(gameObject);
            yield break;
        }
    }
}
