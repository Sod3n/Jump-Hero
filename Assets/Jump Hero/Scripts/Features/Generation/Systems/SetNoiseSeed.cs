using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace GenerationAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class SetNoiseSeed : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<NoiseSettings> _noiseSettings;
        EcsWorld _world;

        float _noiseSeed;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _noiseSettings = _world.GetPool<NoiseSettings>();
            _noiseSeed = Random.Range(float.MinValue, float.MaxValue/4);
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<NoiseSettings>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var noiseSettings = ref _noiseSettings.Get(entity);
                noiseSettings.seed = _noiseSeed;


            }
        }
    }
}

