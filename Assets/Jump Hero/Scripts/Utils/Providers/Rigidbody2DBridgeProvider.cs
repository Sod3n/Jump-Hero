﻿using Scellecs.Morpeh.Providers;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
public sealed class Rigidbody2DBridgeProvider : MonoProvider<Rigidbody2DBridge>
{
}