using AleVerDes.LeoEcsLiteZoo;
using CameraFollowAssembly;
using Leopotam.EcsLite;
using log4net.Util;
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
        EcsQuery<TransformForFollowWithLerp, SpeedOfFollow> _entities;
        EcsPool<TransformForFollowWithLerp> _targetsForFollowWithLerp;
        EcsPool<TransformRef> _transformRefs;
        EcsPool<SpeedOfFollow> _speedsOfFollow;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var transform = ref _transformRefs.Get(entity).Value;

                var targetPosition = _targetsForFollowWithLerp.Get(entity).Value.position;
                var speedOfFollow = _speedsOfFollow.Get(entity);

                transform.position = Vector3.Lerp(transform.position,
                        new Vector3(targetPosition.x, transform.position.y, transform.position.z), Time.deltaTime * speedOfFollow.CurrentValue.x);
                transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x, targetPosition.y, transform.position.z), Time.deltaTime * speedOfFollow.CurrentValue.y);
            }
        }
    }
}

