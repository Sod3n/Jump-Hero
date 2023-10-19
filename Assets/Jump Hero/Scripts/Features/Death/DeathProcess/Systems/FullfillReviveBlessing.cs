using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace DeathProcessAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class FullfillReviveBlessing : IEcsRunSystem
    {
        EcsQuery<ReviveBlessing, KillRequest, TransformRef> _entities;
        EcsPool<ReviveBlessing> _reviveBlessings;
        EcsPool<KillRequest> _killRequests;
        EcsPool<TransformRef> _transformRefs;
        EcsPool<Rigidbody2DRef> _rigidbody2DRefs;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                
                ref var transform = ref _transformRefs.Get(entity).Value;
                var reviveBlessing = _reviveBlessings.Get(entity);

                transform.position = reviveBlessing.Position;

                if(_rigidbody2DRefs.Has(entity))
                {
                    var body = _rigidbody2DRefs.Get(entity).Value;
                    body.velocity = Vector2.zero;
                }

                _killRequests.Del(entity);
                _reviveBlessings.Del(entity);
            }
        }
    }
}

