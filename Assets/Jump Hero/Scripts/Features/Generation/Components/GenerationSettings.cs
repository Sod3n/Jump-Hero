using System;
using System.Collections.Generic;
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
    public struct GenerationSettings
    {
        public Vector2 GenerationSize;
        public Vector2Int SetupSize;
        public GameObject[] Prefabs;
        public Dictionary<Vector2, int> Vector2ToSetupEntity;
    }
}
