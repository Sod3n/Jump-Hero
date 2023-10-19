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

    internal class ActivateSubElements : IEcsInitSystem, IEcsRunSystem
    {
        EcsQuery<ChanceOfActivationSubElement, GameObjectRef> _entities;
        int _noiseSettingsEntity;
        EcsPool<ChanceOfActivationSubElement> _chanceOfActivationSubElement;
        EcsPool<GameObjectRef> _gameObjectRefs;
        EcsPool<NoiseSettings> _noiseSettings;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _noiseSettingsEntity = _world.Filter<NoiseSettings>().Inc<SubElementsNoiseSettingsMarker>().End().GetFirstEntity<NoiseSettings>();
        }
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var chanceOfActivation = ref _chanceOfActivationSubElement.Get(entity);
                ref var gameObjectRef = ref _gameObjectRefs.Get(entity);

                var noiseSettings = _noiseSettings.Get(_noiseSettingsEntity);

                float threshold = GetThreshold(chanceOfActivation.Value, noiseSettings.Octave);
                Vector2 pos = gameObjectRef.Value.transform.position;
                
                Perlin.Seed = noiseSettings.Seed;

                gameObjectRef.Value.SetActive(true);
                if (Perlin.Fbm(pos * noiseSettings.Scale, noiseSettings.Octave) > threshold) continue;

                gameObjectRef.Value.SetActive(false);
                _world.DelEntity(entity);
            }
        }
        private float GetThreshold(float chanceOfActivation, int octave)
        {
            float floor = 0.51f / (2);
            return floor - chanceOfActivation * 2 * floor / 100;
        }
    }
}

