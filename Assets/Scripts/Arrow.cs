using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _damage;

    public float Damage => _damage;

    public void Shoot(float forse, Transform enemy)
    {
        StartCoroutine(Shot(forse, enemy));
    }

    private IEnumerator Shot(float forse, Transform enemy)
    {
        float second = 0.01f;

        var waitTime = new WaitForSeconds(second);

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
