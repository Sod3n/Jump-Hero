using AleVerDes.LeoEcsLiteZoo;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using System.ComponentModel;

namespace MovementByPhysicsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class GenerateOnGroundCollisionEvent : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<OnCollisionExit2DEvent> _onCollisionExit2DEvents;
        EcsPool<OnGroundCollisionEvent> _onGroundCollisionEvents;
        EcsPool<GroundMarker> _groundMarkers;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _onGroundCollisionEvents = _world.GetPool<OnGroundCollisionEvent>();
            _groundMarkers = _world.GetPool<GroundMarker>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<OnCollisionExit2DEvent>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                var onCollisionExitEvent = _onCollisionExit2DEvents.Get(entity);

                if (onCollisionExitEvent.collider2D.gameObject.TryGetEntity(out var groundEntity))
                {
                    if (!_groundMarkers.Has(groundEntity)) continue;
                }
                _onGroundCollisionEvents.Add(entity);
            }
        }
    }
}

