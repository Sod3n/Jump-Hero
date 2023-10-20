using AleVerDes.LeoEcsLiteZoo;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using System.ComponentModel;

namespace CoinsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class AddCoinPullerOnTriggerEnter : IEcsRunSystem
    {
        EcsQuery<OnTriggerEnter2DEvent> _entities;
        EcsPool<CoinPuller> _coinPullers;
        EcsPool<OnTriggerEnter2DEvent> _triggerEnterEvents;
        EcsPool<CoinMarker> _coinMarkers;
        EcsPool<CoinCollectorMarker> _coinCollectorMarkers;
        EcsPool<TransformRef> _transformRefs;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var triggerEnterEvent = _triggerEnterEvents.Get(entity);

                int coinEntity;
                int coinCollectorEntity;
                if(!triggerEnterEvent.collider2D.gameObject.TryGetEntity(out coinEntity)) continue;
                if(!triggerEnterEvent.senderGameObject.TryGetEntity(out coinCollectorEntity)) continue;
                if(!_coinMarkers.Has(coinEntity)) continue;
                if(!_coinCollectorMarkers.Has(coinCollectorEntity)) continue;
                if(!_transformRefs.Has(coinCollectorEntity)) continue;

                if(!_coinPullers.Has(coinEntity))
                    _coinPullers.Add(coinEntity) = new CoinPuller
                    {
                        Value = _transformRefs.Get(coinCollectorEntity).Value
                    };
            }
        }
    }
}

