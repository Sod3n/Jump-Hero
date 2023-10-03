using Scellecs.Morpeh;
using System;
using UnityEngine;




#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

[System.Serializable]
public struct Setup : IComponent 
{
    public SetupType Type;
}
[Serializable]
public enum SetupType
{
    Default,

}