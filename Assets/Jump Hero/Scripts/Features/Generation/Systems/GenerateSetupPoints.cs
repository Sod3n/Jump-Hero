using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;
using UtilsAssembly;

namespace GenerationAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class GenerateSetupPoints : IEcsRunSystem
    {
        EcsQuery<GenerateSetupMarker, Owner, Position2D> _entities;
        EcsPool<GenerationSettings> _genSettings;
        EcsPool<Owner> _owners;
        EcsPool<Position2D> _positions2D;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var owner = _owners.Get(entity);
                if (!owner.Value.Unpack(_world, out int ownerEntity)) continue;
                ref var genSettings = ref _genSettings.Get(ownerEntity);
                var position2D = _positions2D.Get(entity).Value;

                genSettings.Vector2ToSetupEntity ??= new System.Collections.Generic.Dictionary<Vector2, int>();

                //just clamp our coordinates
                float x = position2D.x - position2D.x % genSettings.SetupSize.x;
                float y = position2D.y - position2D.y % genSettings.SetupSize.y;

                Vector2 startPosition2D = new Vector2(x, y);
                if (genSettings.Vector2ToSetupEntity.ContainsKey(startPosition2D)) continue;

                _world.NewEntityWith<SetupPoint>() = new SetupPoint
                {
                    Position = startPosition2D,
                    EntityWithSettings = _world.PackEntity(ownerEntity)
                };
            }
        }
    }
}

