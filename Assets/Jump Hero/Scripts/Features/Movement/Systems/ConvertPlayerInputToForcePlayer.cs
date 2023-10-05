using Leopotam.EcsLite;
using UnityEngine;
using UtilsAssembly;
using static Codice.Client.Common.Connection.AskCredentialsToUser;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    /// <summary>
    /// This System generate entities with ForceTarget component. For deleting this entities responsible another leoecszoo system "DelHere".
    /// </summary>
    internal class ConvertPlayerInputToForcePlayer : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<PlayerActions> _playerActions;
        EcsPool<PowerOfForce> _powersOfForce;
        EcsPool<Force> _forces;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playerActions = world.GetPool<PlayerActions>();
            _powersOfForce = world.GetPool<PowerOfForce>();
            _forces = world.GetPool<Force>();
        }
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            if (_entities is null) _entities = world.Filter<PlayerActions>().Inc<PowerOfForce>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref PlayerActions playerActions = ref _playerActions.Get(entity);
                ref PowerOfForce powerOfForce = ref _powersOfForce.Get(entity);

                Vector2 dir = playerActions.tapping.action.ReadValue<Vector2>();
                if (dir == Vector2.zero) continue;

                int e = world.NewEntity();
                ref Force force = ref _forces.Add(e);
                force.powerOfForce = powerOfForce;
                force.targetOfForce.value = world.PackEntity(entity);
                force.direction2D.value = TapPositionToDirection(dir);
            }
        }

        private Vector2 TapPositionToDirection(Vector2 tapPosition)
        {
            Vector2 dir;
            switch (tapPosition.x)
            {
                case float x when (x > 0 && x <= 160):
                    dir = Vector2.left + Vector2.up * 5;
                    dir.Normalize();
                    return dir;

                case float x when (x > 160 && x <= 320):
                    dir = Vector2.up;
                    return dir;

                case float x when (x > 320 && x <= 480):
                    dir = Vector2.right + Vector2.up * 5;
                    dir.Normalize();
                    return dir;

            }
            return Vector2.zero;
        }
    }
}

