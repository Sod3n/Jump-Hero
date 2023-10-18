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

    internal class ChangeFollowSpeedByRestrictions : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<TransformForFollowWithLerp> _targetsForFollowWithLerp;
        EcsPool<TransformRef> _transformRefs;
        EcsPool<HeightFollowRescriction> _heightFollowRestrictions;
        EcsPool<SpeedOfFollow> _speedsOfFollow;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<TransformForFollowWithLerp>().Inc<SpeedOfFollow>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var speedOfFollow = ref _speedsOfFollow.Get(entity);

                var targetPosition = _targetsForFollowWithLerp.Get(entity).Value.position;

                if (_heightFollowRestrictions.Has(entity))
                {
                    var restriction = _heightFollowRestrictions.Get(entity);

                    if (restriction.startOfSlowHeight - restriction.stopHeight > targetPosition.y)
                    {
                        speedOfFollow.TargetValue = new Vector2(0, 0);
                        continue;
                    }
                        

                    if (restriction.startOfSlowHeight - restriction.slowHeight > targetPosition.y)
                    {
                        speedOfFollow.TargetValue = new Vector2(1, 1 / restriction.slowPower);
                    }
                    else
                        speedOfFollow.TargetValue = new Vector2(1, 1);

                }
                else
                {
                    speedOfFollow.TargetValue = new Vector2(1, 1);
                }
            }
        }
    }
}

