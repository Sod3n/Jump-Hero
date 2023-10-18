using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace CameraFollowAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class LerpSpeedOfFollow : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<SpeedOfFollow> _speedsOfFollow;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {

        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<SpeedOfFollow>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var speedOfFollow = ref _speedsOfFollow.Get(entity);

                speedOfFollow.CurrentValue = Vector2.Lerp(speedOfFollow.CurrentValue, speedOfFollow.TargetValue, Time.deltaTime / speedOfFollow.Lerp);
            }
        }
    }
}

