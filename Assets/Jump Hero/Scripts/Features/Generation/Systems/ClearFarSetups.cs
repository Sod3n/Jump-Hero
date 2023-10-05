using Leopotam.EcsLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace GenerationAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class ClearFarSetups : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<GenerationSettings> _generationSettings;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _generationSettings = _world.GetPool<GenerationSettings>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<GenerationSettings>().End();
            if (_entities is null) return;

            Dictionary<Vector2, bool> isVector2Nessesary = new Dictionary<Vector2, bool>();
            foreach (int entity in _entities)
            {
                var generationSettings = _generationSettings.Get(entity);


            }
        }
    }
}

