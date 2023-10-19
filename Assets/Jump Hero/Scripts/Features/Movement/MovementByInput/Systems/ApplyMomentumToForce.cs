using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class ApplyMomentumToForce : IEcsRunSystem
    {
        EcsQuery<ForceCommand, Momentum> _entities;
        EcsPool<ForceCommand> _forceCommands;
        EcsPool<Momentum> _momentums;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var forceCommand = ref _forceCommands.Get(entity);
                var momentum = _momentums.Get(entity).Value;
                forceCommand.PowerOfForce.Value += momentum * Time.fixedDeltaTime * 25;
            }
        }
    }
}

