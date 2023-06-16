using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromLongDistanceTransition : Transition
{
    [SerializeField] private float _distance;

    private void Update()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) <= _distance)
            NeedTransit = true;
    }
}
