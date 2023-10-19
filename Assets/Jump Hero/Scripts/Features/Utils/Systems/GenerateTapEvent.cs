using AleVerDes.LeoEcsLiteZoo;
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
        EcsQuery<InputActions> _entities;
        EcsPool<InputActions> _inputActions;
        EcsPool<TapDownSelfEvent> _tapDownSelfEvents;
        EcsPool<TapUpSelfEvent> _tapUpSelfEvents;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
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

