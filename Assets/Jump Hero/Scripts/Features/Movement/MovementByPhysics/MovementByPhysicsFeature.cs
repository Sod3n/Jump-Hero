using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using UtilsAssembly;

namespace MovementByPhysicsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class MovementByPhysicsFeature : IEcsFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new TrackLanding())
                .Add(new TrackFalls())
                .Add(new GenerateOnGroundCollisionEvent())
                ;
        }

        public void SetupLateUpdateSystems(IEcsSystems systems)
        {
            systems
                .DelHere<LandedSelfEvent>()
                ;
        }

        public void SetupFixedUpdateSystems(IEcsSystems systems)
        {
        }

        public void SetupInjector(IEcsInjector injector)
        {

        }
    }
}

