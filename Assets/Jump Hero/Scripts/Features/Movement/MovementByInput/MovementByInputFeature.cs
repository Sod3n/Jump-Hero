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
    public class MovementByInputFeature : IEcsUpdateFeature, IEcsFixedUpdateFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new InputActionsToCommands())
                .Add(new ResetStaminaToZeroOnTapUp())
                .Add(new ChangeMomentumOnTapUp())
                .Add(new ResetStaminaOnGround())
                .Add(new ResetVelocityOnLanding())
                .Add(new ResetMomentumOnGround())
                ;
        }
        public void SetupFixedUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new ApplyMomentumToForce())
                .Add(new ForceCommandsHandler())
                .Add(new TrackHeight())
                .Add(new SpendStaminaInAir())
                .DelHereEntityWith<ForceCommand>()
                ;
        }
    }
}

