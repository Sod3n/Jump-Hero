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

    internal class CreateGenerationRectangle : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<GenerationSettings> _generationSettings;
        EcsPool<GenerationRectangle> _generationRectangles;
        EcsPool<TransformRef> _transformRefs;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _generationSettings = _world.GetPool<GenerationSettings>();
            _generationRectangles = _world.GetPool<GenerationRectangle>();
            _transformRefs = _world.GetPool<TransformRef>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<GenerationSettings>().Inc<TransformRef>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {

                var generationSettings = _generationSettings.Get(entity);
                var transformRef = _transformRefs.Get(entity);

                var newGenerationRectangle = new GenerationRectangle();
                newGenerationRectangle.LeftDownCorner = GetGenerationLeftDownCorner(transformRef.Value.position, generationSettings.generationSize);
                newGenerationRectangle.RightUpCorner = GetGenerationRightUpCorner(transformRef.Value.position, generationSettings.generationSize);
                if (!_generationRectangles.Has(entity))
                    _generationRectangles.Add(entity) = newGenerationRectangle;
                ref var generationRectangle = ref _generationRectangles.Get(entity);
                generationRectangle = newGenerationRectangle;

            }
        }
        private Vector2 GetGenerationLeftDownCorner(Vector2 generationCenter, Vector2 generationSize)
        {
            float xStart = generationCenter.x - generationSize.x / 2;
            float yStart = generationCenter.y - generationSize.y / 2;
            return new Vector2(xStart, yStart);
        }
        private Vector2 GetGenerationRightUpCorner(Vector2 generationCenter, Vector2 generationSize)
        {
            float xEnd = generationCenter.x + generationSize.x / 2;
            float yEnd = generationCenter.y + generationSize.y / 2;
            return new Vector2(xEnd, yEnd);
        }
    }
}

