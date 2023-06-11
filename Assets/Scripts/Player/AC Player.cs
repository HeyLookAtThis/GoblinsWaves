using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ACPlayer
{
    public static class State
    {
        public const string Idle = nameof(Idle);
        public const string Shot = nameof(Shot);
    }

    public static class Params
    {
        public const string Shot = nameof(Shot);
    }
}
