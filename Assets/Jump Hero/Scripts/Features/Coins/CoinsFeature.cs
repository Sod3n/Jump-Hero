using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;


namespace CoinsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class CoinsFeature : IEcsFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new AddCoinPullerOnTriggerEnter())
                .Add(new AddCoinToStashOnPullFinish())
                .DelHereEntityWith<PullFinishedSelfEvent>()
                ;
        }

        public void SetupLateUpdateSystems(IEcsSystems systems)
        {
        }

        public void SetupFixedUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new PullCoins());
                ;
        }

        public void SetupInjector(IEcsInjector injector)
        {

        }
    }
}

