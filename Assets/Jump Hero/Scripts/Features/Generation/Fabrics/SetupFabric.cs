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
        private static Dictionary<Vector2, int> _vector2ToEntity = new Dictionary<Vector2, int>();

        private EcsWorld _world;
        private Vector2Int _setupSize;
        private EcsPool<GameObjectRef> _gameObjectRefs;
        private GameObject[] _prefabs;

        private NoiseSettings _noiseSettings;

        public SetupFabric(EcsWorld ecsWorld, Vector2Int setupSize, GameObject[] prefabs, NoiseSettings noiseSettings)
        {
            _world = ecsWorld;
            _setupSize = setupSize;
            _gameObjectRefs = _world.GetPool<GameObjectRef>();
            _prefabs = prefabs;

            _noiseSettings = noiseSettings;
        }

        public void TryCreate(Vector2 position)
        {
            position = GetSetupPoint(position);
            if (_vector2ToEntity.ContainsKey(position)) return;

            int sId = 0;

            _world.NewEntityWith<Setup>(out int entity) = new Setup()
            {
                id = sId,
            };

            _vector2ToEntity.Add(position, entity);

            GameObject setup = Object.Instantiate(_prefabs[sId], position, new Quaternion());
            _gameObjectRefs.Add(entity) = new GameObjectRef
            {
                Value = setup,
            };
        }
        private Vector2 GetSetupPoint(Vector2 insidePosition)
        {
            //just clamp our coordinates
            float x = insidePosition.x - insidePosition.x % _setupSize.x;
            float y = insidePosition.y - insidePosition.y % _setupSize.y;

            return new Vector2(x, y);
        }
    }
}

