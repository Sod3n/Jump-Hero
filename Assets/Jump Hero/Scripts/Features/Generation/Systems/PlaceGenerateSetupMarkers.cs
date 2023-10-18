using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;
using UtilsAssembly;

namespace GenerationAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class PlaceGenerateSetupMarkers : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<GenerationSettings> _genSettings;
        EcsPool<GenerationRectangle> _generationRectangles;
        EcsPool<Position2D> _positions2D;
        EcsPool<Owner> _owners;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {

        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<GenerationSettings>().Inc<GenerationRectangle>().Inc<NoiseSettings>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var genSettings = ref _genSettings.Get(entity);
                var generationRectangle = _generationRectangles.Get(entity);

                for (float x = generationRectangle.LeftDownCorner.x; x <= generationRectangle.RightUpCorner.x; x += genSettings.SetupSize.x)
                    for (float y = generationRectangle.LeftDownCorner.y; y <= generationRectangle.RightUpCorner.y; y += genSettings.SetupSize.y)
                    {
                        _world.NewEntityWith<GenerateSetupMarker>(out int marker);
                        _positions2D.Add(marker) = new Position2D
                        {
                            Value = new UnityEngine.Vector2(x, y)
                        };
                        _owners.Add(marker) = new Owner
                        {
                            Value = _world.PackEntity(entity),
                        };
;                    }
            }
        }
    }
}

