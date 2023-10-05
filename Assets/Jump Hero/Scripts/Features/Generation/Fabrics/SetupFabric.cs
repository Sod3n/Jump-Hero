using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.Collections.Generic;
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


    public class SetupFabric
    {
        public Dictionary<Vector2, int> Vector2ToEntity { get; private set; }

        private EcsWorld _world;
        private Vector2Int _setupSize;
        private EcsPool<GameObjectRef> _gameObjectRefs;
        private GameObject[] _prefabs;

        private NoiseSettings _noiseSettings;

        public SetupFabric(EcsWorld ecsWorld, Vector2Int setupSize, GameObject[] prefabs, NoiseSettings noiseSettings, Dictionary<Vector2, int> vector2ToEntity)
        {
            _world = ecsWorld;
            _setupSize = setupSize;
            _gameObjectRefs = _world.GetPool<GameObjectRef>();
            _prefabs = prefabs;

            _noiseSettings = noiseSettings;
            Vector2ToEntity = vector2ToEntity;
        }

        public void TryCreate(Vector2 atPosition)
        {
            atPosition = GenerationMath.GetSetupPoint(atPosition, _setupSize);
            if (Vector2ToEntity.ContainsKey(atPosition)) return;

            int sId = 0;

            _world.NewEntityWith<Setup>(out int entity) = new Setup()
            {
                id = sId,
            };

            Vector2ToEntity.Add(atPosition, entity);

            GameObject setup = Object.Instantiate(_prefabs[sId], atPosition, new Quaternion());
            _gameObjectRefs.Add(entity) = new GameObjectRef
            {
                Value = setup,
            };
        }
    }
}

