using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class FollowTransformsWithLerp : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<TransformForFollowWithLerp> _targetsForFollowWithLerp;
        EcsPool<TransformRef> _transformRefs;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _targetsForFollowWithLerp = _world.GetPool<TransformForFollowWithLerp>();
            _transformRefs = _world.GetPool<TransformRef>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<TransformForFollowWithLerp>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var transformRef = ref _transformRefs.Get(entity);

                var targetPosition = _targetsForFollowWithLerp.Get(entity).value.position;

                transformRef.Value.position = Vector3.Lerp(transformRef.Value.position, new Vector3(targetPosition.x, targetPosition.y, transformRef.Value.position.z), Time.deltaTime);
                
            }
        }
    }
}

