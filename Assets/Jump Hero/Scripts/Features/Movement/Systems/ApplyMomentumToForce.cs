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
        EcsFilter _entities;
        EcsPool<ForceCommand> _forceCommands;
        EcsPool<Momentum> _momentums;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _forceCommands = _world.GetPool<ForceCommand>();
            _momentums = _world.GetPool<Momentum>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<ForceCommand>().Inc<Momentum>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var forceCommand = ref _forceCommands.Get(entity);
                var momentum = _momentums.Get(entity).Value;
                forceCommand.PowerOfForce.Value += momentum * Time.deltaTime * 100;
            }
        }
    }
}

