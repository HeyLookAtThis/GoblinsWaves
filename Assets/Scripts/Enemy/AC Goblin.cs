using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACGoblin : MonoBehaviour
{
    public static class State
    {
        public const string Idle = nameof(Idle);
        public const string Move = nameof(Move);
        public const string Die = nameof(Die);
        public const string Attack = nameof(Attack);
        public const string TakeDamage = nameof(TakeDamage);
    }

    public static class Params
    {

    }
}
