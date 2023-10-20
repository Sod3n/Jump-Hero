using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace CoinsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class PullCoins : IEcsRunSystem
    {
        EcsQuery<CoinPuller, TransformRef, PullingSettings> _entities;
        EcsPool<CoinPuller> _coinPullers;
        EcsPool<TransformRef> _transformRefs;
        EcsPool<PullingSettings> _pullingSettings;
        EcsPool<PullFinishedSelfEvent> _pullFinishedSelfEvents;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var coinPuller = _coinPullers.Get(entity).Value;
                ref var tranformRef = ref _transformRefs.Get(entity);
                ref var pullingSettings = ref _pullingSettings.Get(entity);

                tranformRef.Value.position = Vector3.Lerp(tranformRef.Value.position, coinPuller.position, pullingSettings.SpeedOfPull * Time.fixedDeltaTime);

                if((coinPuller.position - tranformRef.Value.position).magnitude < pullingSettings.FinishRadius)
                {
                    _pullFinishedSelfEvents.Add(entity);
                    tranformRef.Value.gameObject.SetActive(false);
                }
            }
        }
    }
}

