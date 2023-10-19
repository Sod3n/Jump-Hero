using AleVerDes.LeoEcsLiteZoo;
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
        EcsQuery<InputActions> _entities;
        EcsPool<InputActions> _inputActions;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var inputActions = ref _inputActions.Get(entity);

                inputActions.LastTap = inputActions.Tapping.action.ReadValue<Vector2>();
            }
        }
    }
}

