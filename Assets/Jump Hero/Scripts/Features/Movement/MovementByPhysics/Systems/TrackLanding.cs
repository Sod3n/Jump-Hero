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

    internal class TrackLanding : IEcsRunSystem
    {
        EcsQuery<OnCollisionEnter2DEvent> _enterEvents;
        EcsPool<OnCollisionEnter2DEvent> _onCollisionEnter2DEvents;
        EcsPool<OnGround> _onGrounds;
        EcsPool<GroundMarker> _groundMarkers;
        EcsPool<LandedSelfEvent> _landedSelfEvents;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int enter in _enterEvents)
            {
                var onCollisionEnterEvent = _onCollisionEnter2DEvents.Get(enter);
                
                if (!onCollisionEnterEvent.firstContactPoint2D.enabled) continue; //sure that platform effector not doing its stuff
                
                if(onCollisionEnterEvent.collider2D.gameObject.TryGetEntity(out var groundEntity))
                {
                    if (!_groundMarkers.Has(groundEntity)) continue;
                }
                
                if (onCollisionEnterEvent.senderGameObject.TryGetEntity(out var senderEntity))
                {
                    if (!_onGrounds.Has(senderEntity)) continue;

                    ref var onGround = ref _onGrounds.Get(senderEntity);

                    if (!onGround.Value)
                        _landedSelfEvents.Add(senderEntity) = new LandedSelfEvent
                        {
                            Velocity = onCollisionEnterEvent.relativeVelocity
                        };
                    onGround.Value = true;
                }
            }
        }
    }
}

