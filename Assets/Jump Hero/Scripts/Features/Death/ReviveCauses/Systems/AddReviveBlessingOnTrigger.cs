using Leopotam.EcsLite;
using System.ComponentModel;
using LeoEcsPhysics;
using DeathProcessAssembly;
using AleVerDes.LeoEcsLiteZoo;
using UnityEngine;
using System.Linq;
using Codice.CM.Client.Differences;
using UtilsAssembly;

namespace ReviveCausesAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class AddReviveBlessingOnTrigger : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<OnTriggerEnter2DEvent> _triggerEnterEvents;
        EcsPool<ReviveBlessing> _reviveBlessings;
        EcsPool<ReviveStatueMarker> _reviveStatueMarkers;
        EcsPool<TransformRef> _transformRefs;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<OnTriggerEnter2DEvent>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                var triggerEnterEvent = _triggerEnterEvents.Get(entity);

                int statueEntity;
                if (!triggerEnterEvent.collider2D.gameObject.TryGetEntity(out statueEntity)) continue;
                if (!_reviveStatueMarkers.Has(statueEntity)) continue;

                if (!_transformRefs.Has(statueEntity)) continue;

                Vector2 statuePosition = _transformRefs.Get(statueEntity).Value.position;
                int movingEntity;
                if (!triggerEnterEvent.senderGameObject.TryGetEntity(out movingEntity)) continue;

                if (_reviveBlessings.Has(movingEntity)) _reviveBlessings.Del(movingEntity);
                _reviveBlessings.Add(movingEntity) = new ReviveBlessing
                {
                    Position = statuePosition
                };
                _reviveStatueMarkers.Del(statueEntity);
            }
        }
    }
}

