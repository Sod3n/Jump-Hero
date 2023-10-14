using AleVerDes.LeoEcsLiteZoo;
using DeathProcessAssembly;
using Leopotam.EcsLite;
using System.ComponentModel;
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
        EcsFilter _entities;
        EcsPool<Rigidbody2DRef> _rigidbody2DRefs;
        EcsPool<KillRequest> _killRequests;
        EcsPool<MinVelocityToKilledByLanding> _minVelocityToKilledByLanding;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _rigidbody2DRefs = _world.GetPool<Rigidbody2DRef>();
            _killRequests = _world.GetPool<KillRequest>();
            _minVelocityToKilledByLanding = _world.GetPool<MinVelocityToKilledByLanding>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<Rigidbody2DRef>().Inc<LandedSelfEvent>().Inc<MinVelocityToKilledByLanding>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                var body = _rigidbody2DRefs.Get(entity).Value;
                var minVelocity = _minVelocityToKilledByLanding.Get(entity).Value;
                if (body.velocity.magnitude >= minVelocity) _killRequests.Add(entity);
            }
        }
    }
}

