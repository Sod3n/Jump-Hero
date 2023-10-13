using System;
using UnityEngine;

namespace CameraFollowAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SpeedOfFollow
    {
        [NonSerialized] public Vector2 CurrentValue;
        public Vector2 TargetValue;
        [Range(0.01f, float.MaxValue)]
        public float Lerp;
    }
}
