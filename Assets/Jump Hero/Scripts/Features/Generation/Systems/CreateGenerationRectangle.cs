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
        EcsQuery<GenerationSettings, TransformRef> _entities;
        EcsPool<GenerationSettings> _generationSettings;
        EcsPool<GenerationRectangle> _generationRectangles;
        EcsPool<TransformRef> _transformRefs;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {

                var generationSettings = _generationSettings.Get(entity);
                var transformRef = _transformRefs.Get(entity);

                var newGenerationRectangle = new GenerationRectangle();
                newGenerationRectangle.LeftDownCorner = GetGenerationLeftDownCorner(transformRef.Value.position, generationSettings.GenerationSize);
                newGenerationRectangle.RightUpCorner = GetGenerationRightUpCorner(transformRef.Value.position, generationSettings.GenerationSize);
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

