using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
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

    internal class AddForcesToRigidbody2D : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<Force> _forces;
        EcsPool<Rigidbody2DRef> _bodies;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _forces = world.GetPool<Force>();
            _bodies = world.GetPool<Rigidbody2DRef>();
        }
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            if (_entities is null) _entities = world.Filter<Force>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref Force force = ref _forces.Get(entity);
                if (!force.targetOfForce.value.Unpack(world, out int e)) continue;
                if (!_bodies.Has(e)) continue;
                ref Rigidbody2DRef body = ref _bodies.Get(e);
                body.Value.AddForce(force.powerOfForce.value * force.direction2D.value, ForceMode2D.Force);
                Debug.Log(force.powerOfForce.value * force.direction2D.value);
            }
        }
    }
}

