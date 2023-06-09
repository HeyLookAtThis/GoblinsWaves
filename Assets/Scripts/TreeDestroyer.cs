using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Tree>(out Tree tree))
            tree.Destroy();
    }
}
