using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using MovementAssembly;
using System.ComponentModel;

namespace CameraFollowAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class ConvertMaxHeightOfFollowedToRestrictions : IEcsRunSystem
    {
        EcsQuery<HeightFollowRescriction, TransformForFollowWithLerp> _entities;
        EcsPool<HeightFollowRescriction> _heightFollowRescrictions;
        EcsPool<TransformForFollowWithLerp> _transformForFollowsWithLerp;
        EcsPool<MaxReachedHeight> _maxReachedHeights;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var transformForFollow = _transformForFollowsWithLerp.Get(entity).Value;
                ref var restriction = ref _heightFollowRescrictions.Get(entity);

                if(transformForFollow.TryGetEntity(out int followedEntity))
                {
                    if(!_maxReachedHeights.Has(followedEntity)) continue;

                    var maxHeight = _maxReachedHeights.Get(followedEntity).Value;
                    restriction.startOfSlowHeight = maxHeight;
                }
            }
        }
    }
}

