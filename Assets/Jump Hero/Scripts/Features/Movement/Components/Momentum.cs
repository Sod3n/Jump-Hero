using System;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct Momentum
    {
        public float IncreaseRate;
        public float ResetTime;
        [NonSerialized] public float Value;
        [NonSerialized] public float ResetCurrentTime;
    }
}
