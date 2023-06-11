using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransition : Transition
{
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;

    public float Distance { get; private set; }

    private void Start()
    {
        Distance = Random.Range(_minDistance, _maxDistance);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) <= Distance)
            NeedTransit = true;
    }
}
