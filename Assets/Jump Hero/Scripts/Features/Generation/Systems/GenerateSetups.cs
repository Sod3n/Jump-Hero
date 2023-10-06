using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine.UIElements;
using UnityEngine;
using Codice.CM.Client.Differences;
using AleVerDes.LeoEcsLiteZoo;
using UtilsAssembly;
using static UnityEditor.PlayerSettings;

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
        EcsPool<NoiseSettings> _noiseSettings;
        EcsPool<Owner> _owners;
        EcsPool<Position2D> _positions2D;
        EcsPool<GameObjectRef> _gameObjectRefs;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _genSettings = _world.GetPool<GenerationSettings>();
            _noiseSettings = _world.GetPool<NoiseSettings>();
            _positions2D = _world.GetPool<Position2D>();
            _owners = _world.GetPool<Owner>();
            _gameObjectRefs = _world.GetPool<GameObjectRef>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<GenerateSetupMarker>().Inc<Owner>().Inc<Position2D>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                var owner = _owners.Get(entity);
                if(!owner.Value.Unpack(_world, out int ownerEntity)) continue;
                ref var genSettings = ref _genSettings.Get(ownerEntity);
                var position2D = _positions2D.Get(entity).Value;

                var noiseSettings = _noiseSettings.Get(ownerEntity);
                genSettings.Vector2ToSetupEntity ??= new System.Collections.Generic.Dictionary<Vector2, int>();

                position2D = GenerationMath.GetSetupPoint(position2D, genSettings.SetupSize);
                if (genSettings.Vector2ToSetupEntity.ContainsKey(position2D)) continue;

                int sId = 0;

                _world.NewEntityWith<Setup>(out int setupEnt) = new Setup()
                {
                    Id = sId,
                };

                genSettings.Vector2ToSetupEntity.Add(position2D, setupEnt);

                GameObject setup = Object.Instantiate(genSettings.Prefabs[sId], position2D, new Quaternion());
                _gameObjectRefs.Add(entity) = new GameObjectRef
                {
                    Value = setup,
                };
            }
        }
    }
}