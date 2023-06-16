using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACSkeleton : MonoBehaviour
{
    public static class State
    {
        public const string Idle = nameof(Idle);
        public const string Walk = nameof(Walk);
        public const string Death = nameof(Death);
        public const string Attack = nameof(Attack);
        public const string TakeDamage = nameof(TakeDamage);
    }

    public static class Params
    {
        public const string Speed = nameof(Speed);
    }
}
