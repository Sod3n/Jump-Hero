using System;
using UnityEngine;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct HeightFollowRescriction
    {
        [NonSerialized] public float startOfSlowHeight;
        [Range(0.01f, float.MaxValue)]
        public float slowPower;
        public float stopHeight;
        public float slowHeight;
    }
}
