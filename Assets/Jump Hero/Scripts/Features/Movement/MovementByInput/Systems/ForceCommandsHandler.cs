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

    internal class ForceCommandsHandler : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<ForceCommand> _forces;
        EcsPool<Rigidbody2DRef> _bodies;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _forces = world.GetPool<ForceCommand>();
            _bodies = world.GetPool<Rigidbody2DRef>();
        }
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            if (_entities is null) _entities = world.Filter<ForceCommand>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref ForceCommand force = ref _forces.Get(entity);
                if (!force.TargetOfForce.Value.Unpack(world, out int e)) continue;
                if (!_bodies.Has(e)) continue;
                ref Rigidbody2DRef body = ref _bodies.Get(e);
                body.Value.AddForce(force.PowerOfForce.Value * force.Direction2D.Value, ForceMode2D.Force);
            }
        }
    }
}

