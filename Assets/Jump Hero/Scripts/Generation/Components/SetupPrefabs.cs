using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
public struct SetupPrefabs : IComponent
{
    public SetupType[] setupTypes;
    public GameObject[] prefabs;
}