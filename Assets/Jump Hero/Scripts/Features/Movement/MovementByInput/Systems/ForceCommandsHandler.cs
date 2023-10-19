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
        EcsQuery<ForceCommand> _entities;
        EcsPool<ForceCommand> _forces;
        EcsPool<Rigidbody2DRef> _bodies;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
        }
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref ForceCommand force = ref _forces.Get(entity);
                if (!force.TargetOfForce.Value.Unpack(_world, out int e)) continue;
                if (!_bodies.Has(e)) continue;
                ref Rigidbody2DRef body = ref _bodies.Get(e);
                body.Value.AddForce(force.PowerOfForce.Value * force.Direction2D.Value, ForceMode2D.Force);
            }
        }
    }
}

