using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;


namespace ChestAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class ChestFeature : IEcsFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new OpenChestOnTriggerEnter())
                .Add(new SpawnContentOnChestOpenned())
                .DelHereEntityWith<ChestOpennedSelfEvent>()
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

