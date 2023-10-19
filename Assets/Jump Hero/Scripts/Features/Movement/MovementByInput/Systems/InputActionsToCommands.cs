using AleVerDes.LeoEcsLiteZoo;
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
    internal class InputActionsToCommands : IEcsRunSystem
    {
        EcsQuery<InputActions, PowerOfForce> _entities;
        EcsPool<InputActions> _playerActions;
        EcsPool<PowerOfForce> _powersOfForce;
        EcsPool<ForceCommand> _forces;
        EcsPool<Stamina> _stamina;
        EcsPool<Momentum> _momentums;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                InputActions playerActions = _playerActions.Get(entity);
                PowerOfForce powerOfForce = _powersOfForce.Get(entity);

                Vector2 dir = playerActions.Tapping.action.ReadValue<Vector2>();
                if (dir == Vector2.zero) continue;

                if (_stamina.Has(entity))
                {
                    var stamina = _stamina.Get(entity);
                    if (stamina.CurrentValue <= 0) continue;
                }

                int e = _world.NewEntity();
                ref ForceCommand force = ref _forces.Add(e);
                force.PowerOfForce = powerOfForce;

                if (_momentums.Has(entity))
                {
                    _momentums.Add(e) = _momentums.Get(entity);
                }

                force.PowerOfForce.Value *= Time.fixedDeltaTime * 100;
                force.TargetOfForce.Value = _world.PackEntity(entity);
                force.Direction2D.Value = TapPositionToDirection(dir);
            }
        }

        private Vector2 TapPositionToDirection(Vector2 tapPosition)
        {
            int width = Display.main.systemWidth;
            Vector2 dir;
            switch (tapPosition.x)
            {
                case float x when (x > 0 && x <= width * 0.3333):
                    dir = Vector2.left + Vector2.up * 5;
                    dir.Normalize();
                    return dir;

                case float x when (x > width * 0.3333 && x <= width * 0.6666):
                    dir = Vector2.up;
                    return dir;

                case float x when (x > width * 0.6666 && x <= width):
                    dir = Vector2.right + Vector2.up * 5;
                    dir.Normalize();
                    return dir;

            }
            return Vector2.zero;
        }
    }
}

