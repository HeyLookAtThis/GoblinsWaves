using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private float _speed;

    private void Awake()
    {
        StartCoroutine(Mover());
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator Mover()
    {
        float seconds = 0.05f;
        var waitForSeconds = new WaitForSeconds(seconds);

        while (true)
        {
            transform.Translate(Vector3.forward * _speed * seconds, Space.World);
            yield return waitForSeconds;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<TreeDestroyer>(out TreeDestroyer destroyer))
    //        Destroy(gameObject);            
    //}


    private void OnDestroy()
    {
        StopCoroutine(Mover());
    }
}
