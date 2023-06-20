using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FromLowDistanceTransitionn : Transition
{
    [SerializeField] private float _distance;

    private void Update()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) > _distance)
            NeedTransit = true;
    }
}
