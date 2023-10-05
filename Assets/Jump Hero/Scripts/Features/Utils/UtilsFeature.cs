using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;


namespace UtilsAssembly
{
    #if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    #endif
    public class UtilsFeature : IEcsFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {

    #if UNITY_EDITOR
            systems
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem());
    #endif
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

