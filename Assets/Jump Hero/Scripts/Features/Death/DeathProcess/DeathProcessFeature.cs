using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;


namespace DeathProcessAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class DeathProcessFeature : IEcsLateUpdateFeature
    {
        public void SetupLateUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new FullfillReviveBlessing())
                .Add(new RestartSceneOnKillRequest())
                .DelHere<KillRequest>()
                ;
        }
    }
}

