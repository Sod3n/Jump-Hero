using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using MovementByPhysicsAssembly;
using System.ComponentModel;
using UnityEngine;
using UtilsAssembly;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class ResetVelocityOnLanding : IEcsRunSystem
    {
        EcsQuery<Rigidbody2DRef, LandedSelfEvent> _entities;
        EcsPool<Rigidbody2DRef> _rigidbody2DRefs;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var body = ref _rigidbody2DRefs.Get(entity);
                body.Value.velocity = Vector3.zero;
            }
        }
    }
}

