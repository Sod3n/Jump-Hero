using AleVerDes.LeoEcsLiteZoo;
using DeathProcessAssembly;
using Leopotam.EcsLite;
using MovementByPhysicsAssembly;
using System.ComponentModel;
using UnityEngine;
using UtilsAssembly;

namespace DeathCausesAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class DieOnLandingWithSpecificVelocity : IEcsRunSystem
    {
        EcsQuery<LandedSelfEvent, MinVelocityToKilledByLanding> _entities;
        EcsPool<LandedSelfEvent> _landedSelfEvents;
        EcsPool<KillRequest> _killRequests;
        EcsPool<MinVelocityToKilledByLanding> _minVelocityToKilledByLanding;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var landingVelocity = _landedSelfEvents.Get(entity).Velocity;
                var minVelocity = _minVelocityToKilledByLanding.Get(entity).Value;

                if (landingVelocity.magnitude >= minVelocity && !_killRequests.Has(entity)) 
                    _killRequests.Add(entity);
            }
        }
    }
}

