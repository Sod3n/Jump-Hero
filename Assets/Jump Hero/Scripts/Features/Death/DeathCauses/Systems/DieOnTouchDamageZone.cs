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

    internal class DieOnTouchDamageZone : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<OnCollisionEnter2DEvent> _onCollisionEnter2DEvents;
        EcsPool<DamageZoneMarker> _damageZones;
        EcsPool<KillRequest> _killRequests;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {

        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<OnCollisionEnter2DEvent>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                var onCollisionEnterEvent = _onCollisionEnter2DEvents.Get(entity);

                if (!onCollisionEnterEvent.firstContactPoint2D.enabled) continue; //collider effector working so skip

                int collisionEntity;
                int senderEntity;
                if (!onCollisionEnterEvent.collider2D.gameObject.TryGetEntity(out collisionEntity)) continue;
                if (!onCollisionEnterEvent.senderGameObject.TryGetEntity(out senderEntity)) continue;

                if (_damageZones.Has(collisionEntity))
                        _killRequests.Add(senderEntity);
            }
        }
    }
}

