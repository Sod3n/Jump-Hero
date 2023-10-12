using AleVerDes.LeoEcsLiteZoo;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace UtilsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class TrackOnGround : IEcsRunSystem
    {
        EcsFilter _enterEvents;
        EcsFilter _exitEvents;
        EcsPool<OnCollisionEnter2DEvent> _onCollisionEnter2DEvents;
        EcsPool<OnCollisionExit2DEvent> _onCollisionExit2DEvents;
        EcsPool<OnGround> _onGrounds;
        EcsPool<GroundMarker> _groundMarkers;
        EcsPool<LandedSelfEvent> _landedSelfEvents;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _onCollisionEnter2DEvents = _world.GetPool<OnCollisionEnter2DEvent>();
            _onGrounds = _world.GetPool<OnGround>();
            _groundMarkers = _world.GetPool<GroundMarker>();
            _landedSelfEvents = _world.GetPool<LandedSelfEvent>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_enterEvents is null) _enterEvents = _world.Filter<OnCollisionEnter2DEvent>().End();
            if (_enterEvents is null) return;

            foreach (int enter in _enterEvents)
            {
                var onCollisionEnterEvent = _onCollisionEnter2DEvents.Get(enter);

                if(!onCollisionEnterEvent.firstContactPoint2D.enabled) continue; //sure that platform effector not doing its stuff
                
                if(onCollisionEnterEvent.collider2D.gameObject.TryGetEntity(out var groundEntity))
                {
                    if (!_groundMarkers.Has(groundEntity)) continue;
                }

                if(onCollisionEnterEvent.senderGameObject.TryGetEntity(out var senderEntity))
                {
                    if (!_onGrounds.Has(senderEntity)) continue;
                    ref var onGround = ref _onGrounds.Get(senderEntity);
                    if (!onGround.Value) _landedSelfEvents.Add(senderEntity);
                    onGround.Value = true;
                }
            }

            if (_exitEvents is null) _exitEvents = _world.Filter<OnCollisionExit2DEvent>().End();
            if (_exitEvents is null) return;

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

