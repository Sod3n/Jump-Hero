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

    internal class ResetStaminaOnGround : IEcsRunSystem
    {
        EcsQuery<Stamina, OnGround, TapDownSelfEvent> _entities;
        EcsPool<Stamina> _staminas;
        EcsPool<OnGround> _onGrounds;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var stamina = ref _staminas.Get(entity);
                var onGround = _onGrounds.Get(entity).Value;

                if (onGround) stamina.CurrentValue = stamina.MaxValue;
            }
        }
    }
}

