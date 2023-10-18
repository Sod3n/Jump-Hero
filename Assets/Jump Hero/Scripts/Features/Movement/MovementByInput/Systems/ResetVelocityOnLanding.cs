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
        EcsFilter _entities;
        EcsPool<Rigidbody2DRef> _rigidbody2DRefs;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<Rigidbody2DRef>().Inc<LandedSelfEvent>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var body = ref _rigidbody2DRefs.Get(entity);
                body.Value.velocity = Vector3.zero;
            }
        }
    }
}

