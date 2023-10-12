using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;


namespace MovementAssembly
{
    #if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    #endif
    public class MovementFeature : IEcsFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new InputActionsToCommands())
                .Add(new ApplyMomentumToForce())
                .Add(new ForceCommandsHandler())
                .Add(new TrackHeight())
                .Add(new ResetVelocityOnLanding())
                .Add(new ResetStaminaToZeroOnTapUp())
                .Add(new ResetMomentumOnGround())
                .Add(new ChangeMomentumOnTapUp())
                .Add(new ResetStaminaOnGround())
                .Add(new SpendStaminaInAir())
                .DelHereEntityWith<ForceCommand>()
                ;
        }

        public void SetupLateUpdateSystems(IEcsSystems systems)
        {
        }

        public void SetupFixedUpdateSystems(IEcsSystems systems)
        {
        }

        public void SetupInjector(IEcsInjector injector)
        {

        }
    }
}

