using AleVerDes.LeoEcsLiteZoo;
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

    internal class SetNoiseSeed : IEcsInitSystem, IEcsRunSystem
    {
        EcsQuery<NoiseSettings> _entities;
        EcsPool<NoiseSettings> _noiseSettings;
        EcsWorld _world;

        float _noiseSeed;

        public void Init(IEcsSystems systems)
        {
            _noiseSeed = Random.Range(float.MinValue, float.MaxValue/4);
        }
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var noiseSettings = ref _noiseSettings.Get(entity);
                noiseSettings.Seed = _noiseSeed;
            }
        }
    }
}

