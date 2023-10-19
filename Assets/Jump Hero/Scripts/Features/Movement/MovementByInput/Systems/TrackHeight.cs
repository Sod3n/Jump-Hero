using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using log4net.Util;
using System.ComponentModel;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class TrackHeight : IEcsRunSystem
    {
        EcsQuery<MaxReachedHeight, TransformRef> _entities;
        EcsPool<MaxReachedHeight> _maxReachedHeights;
        EcsPool<TransformRef> _transformRefs;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                var tranform = _transformRefs.Get(entity).Value;
                ref var maxReachedHeight = ref _maxReachedHeights.Get(entity);

                if(maxReachedHeight.Value < tranform.position.y)
                    maxReachedHeight.Value = tranform.position.y;
            }
        }
    }
}

