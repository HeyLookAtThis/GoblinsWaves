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
        public const string CelebrateFirst = nameof(CelebrateFirst);
        public const string CelebrateSecond = nameof(CelebrateSecond);
        public const string CelebrateThird = nameof(CelebrateThird);
    }

    public static class Params
    {
        public const string Speed = nameof(Speed);
    }
}
