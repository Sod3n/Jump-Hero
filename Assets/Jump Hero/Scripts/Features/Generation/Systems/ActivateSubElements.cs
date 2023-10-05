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

    internal class ActivateSubElements : IEcsRunSystem
    {
        EcsFilter _entities;
        int _noiseSettingsEntity;
        EcsPool<ChanceOfActivationSubElement> _chanceOfActivationSubElement;
        EcsPool<GameObjectRef> _gameObjectRefs;
        EcsPool<NoiseSettings> _noiseSettings;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _chanceOfActivationSubElement = _world.GetPool<ChanceOfActivationSubElement>();
            _gameObjectRefs = _world.GetPool<GameObjectRef>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<ChanceOfActivationSubElement>().Inc<GameObjectRef>().End();
            if (_entities is null) return;
            _noiseSettingsEntity = _world.Filter<NoiseSettings>().Inc<SubElementsNoiseSettingsMarker>().End().GetFirstEntity<NoiseSettings>();

            foreach (int entity in _entities)
            {
                ref var chanceOfActivation = ref _chanceOfActivationSubElement.Get(entity);
                ref var gameObjectRef = ref _gameObjectRefs.Get(entity);

                var noiseSettings = _noiseSettings.Get(_noiseSettingsEntity);

                float threshold = 1 - chanceOfActivation.value * 2 / 100;
                Vector2 pos = gameObjectRef.Value.transform.position;

                Perlin.Seed = noiseSettings.seed;

                gameObjectRef.Value.SetActive(true);
                if (Perlin.Fbm(pos, noiseSettings.octave) > threshold) continue;

                gameObjectRef.Value.SetActive(false);
                _world.DelEntity(entity);
            }
        }
    }
}

