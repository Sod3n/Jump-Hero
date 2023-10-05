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
                
                .Add(new FollowTransformsWithLerp())
                ;
        }

        public void SetupLateUpdateSystems(IEcsSystems systems)
        {
        }

        public void SetupFixedUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new ConvertPlayerInputToForcePlayer())
                .Add(new AddForcesToRigidbody2D())
                .DelHere<Force>()
                ;
        }

        public void SetupInjector(IEcsInjector injector)
        {

        }
    }
}

