using AleVerDes.LeoEcsLiteZoo;
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

    internal class AddCoinToStashOnPullFinish : IEcsRunSystem
    {
        EcsQuery<PullFinishedSelfEvent, CoinPuller> _entities;
        EcsPool<CoinPuller> _coinPullers;
        EcsPool<CoinStash> _coinStashs;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var coinPuller = _coinPullers.Get(entity).Value;

                int coinCollector;
                if (!coinPuller.TryGetEntity(out coinCollector)) continue;
                if (!_coinStashs.Has(coinCollector)) continue;

                ref var coinStash = ref _coinStashs.Get(coinCollector);
                coinStash.CoinsCount++;
            }
        }
    }
}

