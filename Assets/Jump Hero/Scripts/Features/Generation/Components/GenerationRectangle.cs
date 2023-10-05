using System;
using UnityEngine;

namespace GenerationAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct GenerationRectangle
    {
        public Vector2 LeftDownCorner;
        public Vector2 RightUpCorner;
    }
}
