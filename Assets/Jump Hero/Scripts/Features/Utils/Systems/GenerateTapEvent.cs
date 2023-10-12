using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

namespace UtilsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class GenerateTapEvent : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<InputActions> _inputActions;
        EcsPool<TapDownSelfEvent> _tapDownSelfEvents;
        EcsPool<TapUpSelfEvent> _tapUpSelfEvents;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _inputActions = _world.GetPool<InputActions>();
            _tapDownSelfEvents = _world.GetPool<TapDownSelfEvent>();
            _tapUpSelfEvents = _world.GetPool<TapUpSelfEvent>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<InputActions>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                var inputActions = _inputActions.Get(entity);
                Vector2 tap = inputActions.Tapping.action.ReadValue<Vector2>();
                
                if(tap != Vector2.zero && inputActions.LastTap == Vector2.zero)
                {
                    _tapDownSelfEvents.Add(entity);
                }
                if(tap == Vector2.zero && inputActions.LastTap != Vector2.zero)
                {
                    _tapUpSelfEvents.Add(entity);
                }
            }
        }
    }
}

