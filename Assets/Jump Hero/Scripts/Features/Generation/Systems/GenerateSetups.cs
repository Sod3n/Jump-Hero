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
        EcsQuery<SetupPoint> _entities;
        EcsPool<SetupPoint> _setupPoints;
        EcsPool<GameObjectRef> _gameObjectRefs;
        EcsPool<GenerationSettings> _genSettings;
        EcsPool<NoiseSettings> _noiseSettings;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var setupPoint = _setupPoints.Get(entity);

                int settingsEntity;
                if (!setupPoint.EntityWithSettings.Unpack(_world, out settingsEntity)) continue;

                if(!_genSettings.Has(settingsEntity) || !_noiseSettings.Has(settingsEntity)) continue;

                ref var genSettings = ref _genSettings.Get(settingsEntity);
                var noiseSettings = _noiseSettings.Get(settingsEntity);

                if (genSettings.Vector2ToSetupEntity.ContainsKey(setupPoint.Position)) continue;

                int sId = 0;

                _world.NewEntityWith<Setup>(out int setupEnt) = new Setup()
                {
                    Id = sId,
                };

                genSettings.Vector2ToSetupEntity.Add(setupPoint.Position, setupEnt);

                GameObject setup = Object.Instantiate(genSettings.Prefabs[sId], setupPoint.Position, new Quaternion());
                _gameObjectRefs.Add(entity) = new GameObjectRef
                {
                    Value = setup,
                };
            }
        }
    }
}