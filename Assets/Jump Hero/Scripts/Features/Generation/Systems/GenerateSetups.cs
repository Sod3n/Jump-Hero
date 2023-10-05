using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine.UIElements;
using UnityEngine;
using Codice.CM.Client.Differences;
using AleVerDes.LeoEcsLiteZoo;

namespace GenerationAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    internal class GenerateSetups : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<GenerationSettings> _genSettings;
        EcsPool<TransformRef> _transformRefs;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _genSettings = _world.GetPool<GenerationSettings>();
            _transformRefs = _world.GetPool<TransformRef>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<GenerationSettings>().Inc<TransformRef>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var genSettings = ref _genSettings.Get(entity);
                ref var transformRef = ref _transformRefs.Get(entity);
                var setupFabric = new SetupFabric(_world, genSettings.setupSize, genSettings.prefabs);

                Vector2 genStart = GetGenerationStart(transformRef.Value.position, genSettings.generationSize);
                Vector2 genEnd = GetGenerationEnd(transformRef.Value.position, genSettings.generationSize);
                for (float x = genStart.x; x <= genEnd.x; x += genSettings.setupSize.x)
                    for (float y = genStart.y; y <= genEnd.y; y += genSettings.setupSize.y)
                        setupFabric.TryCreate(new Vector2(x, y));
            }
        }

        private Vector2 GetGenerationStart(Vector2 generationCenter, Vector2 generationSize)
        {
            float xStart = generationCenter.x - generationSize.x / 2;
            float yStart = generationCenter.y - generationSize.y / 2;
            return new Vector2(xStart, yStart);
        }
        private Vector2 GetGenerationEnd(Vector2 generationCenter, Vector2 generationSize)
        {
            float xEnd = generationCenter.x + generationSize.x / 2;
            float yEnd = generationCenter.y + generationSize.y / 2;
            return new Vector2(xEnd, yEnd);
        }
    }
}