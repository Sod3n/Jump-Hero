using System;
using UnityEngine;

namespace MovementByPhysicsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct LandedSelfEvent
    {
        /// <summary>
        /// Velocity of rigidbody2D with which its landed on surface.
        /// </summary>
        public Vector2 Velocity;
    }
}
