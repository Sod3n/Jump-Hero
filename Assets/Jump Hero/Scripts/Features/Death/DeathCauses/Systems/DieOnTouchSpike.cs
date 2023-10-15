using AleVerDes.LeoEcsLiteZoo;
using DeathProcessAssembly;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using System.ComponentModel;

namespace DeathCausesAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class DieOnTouchSpike : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<OnCollisionEnter2DEvent> _onCollisionEnter2DEvents;
        EcsPool<SpikeMarker> _spikeMarkers;
        EcsPool<KillRequest> _killRequests;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _onCollisionEnter2DEvents = _world.GetPool<OnCollisionEnter2DEvent>();
            _spikeMarkers = _world.GetPool<SpikeMarker>();
            _killRequests = _world.GetPool<KillRequest>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<OnCollisionEnter2DEvent>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                var onCollisionEnterEvent = _onCollisionEnter2DEvents.Get(entity);

                if (!onCollisionEnterEvent.firstContactPoint2D.enabled) continue; //collider effector working so skip

                if(onCollisionEnterEvent.collider2D.gameObject.TryGetEntity(out int collisionEntity))
                {
                    if(_spikeMarkers.Has(collisionEntity))
                        _killRequests.Add(entity);
                }
            }
        }
    }
}

