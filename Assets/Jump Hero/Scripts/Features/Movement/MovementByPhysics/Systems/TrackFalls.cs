using AleVerDes.LeoEcsLiteZoo;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace MovementByPhysicsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class TrackFalls : IEcsRunSystem
    {
        EcsQuery<OnCollisionExit2DEvent> _exitEvents;
        EcsPool<OnCollisionExit2DEvent> _onCollisionExit2DEvents;
        EcsPool<OnGround> _onGrounds;
        EcsPool<GroundMarker> _groundMarkers;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int exit in _exitEvents)
            {
                var onCollisionExitEvent = _onCollisionExit2DEvents.Get(exit);

                if (onCollisionExitEvent.collider2D.gameObject.TryGetEntity(out var groundEntity))
                {
                    if (!_groundMarkers.Has(groundEntity)) continue;
                }
                
                if (onCollisionExitEvent.senderGameObject.TryGetEntity(out var senderEntity))
                {
                    if (!_onGrounds.Has(senderEntity)) continue;
                    ref var onGround = ref _onGrounds.Get(senderEntity);
                    onGround.Value = false;
                }
            }
        }
    }
}

