﻿using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System;
using UnityEngine;

[System.Serializable]
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

public struct OnGround : IComponent
{
    public bool value;
}
