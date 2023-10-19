using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using MovementByPhysicsAssembly;
using System.ComponentModel;
using UnityEngine;
using UtilsAssembly;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class ResetMomentumOnGround : IEcsRunSystem
    {
        EcsQuery<OnGround, Momentum> _entities;
        EcsPool<Momentum> _momentums;
        EcsPool<OnGround> _onGrounds;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var momentum = ref _momentums.Get(entity);
                var onGround = _onGrounds.Get(entity).Value;

                if (!onGround) continue;

                if(momentum.ResetCurrentTime <= 0) momentum.Value = 0;
                else momentum.ResetCurrentTime -= Time.fixedDeltaTime;
            }
        }
    }
}

