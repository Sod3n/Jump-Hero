using Scellecs.Morpeh;
using UnityEngine;

[System.Serializable]
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
public struct ForceTarget : IComponent
{
    public float power;
    public Entity target;
}