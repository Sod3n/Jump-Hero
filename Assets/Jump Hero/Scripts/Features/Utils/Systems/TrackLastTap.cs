using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace UtilsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class TrackLastTap : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<InputActions> _inputActions;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _inputActions = _world.GetPool<InputActions>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<InputActions>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var inputActions = ref _inputActions.Get(entity);

                inputActions.LastTap = inputActions.Tapping.action.ReadValue<Vector2>();
            }
        }
    }
}

