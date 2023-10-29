using AleVerDes.LeoEcsLiteZoo;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using System.ComponentModel;

namespace ChestAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class OpenChestOnTriggerEnter : IEcsRunSystem
    {
        EcsQuery<OnTriggerEnter2DEvent> _entities;
        EcsPool<OnTriggerEnter2DEvent> _onTriggerEnter2DEvents;
        EcsPool<ChestOpennerMarker> _chestOpennerMarkers;
        EcsPool<ChestContents> _chestContents;
        EcsPool<ChestOpennedSelfEvent> _chestOpennedSelfEvents;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var triggerEvent = _onTriggerEnter2DEvents.Get(entity);

                int oppenerEntity;
                int chestEntity;
                if(!triggerEvent.senderGameObject.TryGetEntity(out oppenerEntity)) continue;
                if(!triggerEvent.collider2D.TryGetEntity(out chestEntity)) continue;

                if(!_chestOpennerMarkers.Has(oppenerEntity)) continue;
                if(!_chestContents.Has(chestEntity)) continue;

                _chestOpennedSelfEvents.Add(chestEntity);
            }
        }
    }
}

