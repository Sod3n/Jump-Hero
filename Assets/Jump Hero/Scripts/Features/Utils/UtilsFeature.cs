using AleVerDes.LeoEcsLiteZoo;
using LeoEcsPhysics;
using Leopotam.EcsLite;


namespace UtilsAssembly
{
    #if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    #endif
    public class UtilsFeature : IEcsUpdateFeature, IEcsLateUpdateFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new GenerateTapEvent())
                .Add(new TrackLastTap())
#if UNITY_EDITOR

                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                ;
        }

        public void SetupLateUpdateSystems(IEcsSystems systems)
        {
            systems
                .DelHere<OnCollisionEnter2DEvent>()
                .DelHere<OnCollisionExit2DEvent>()
                .DelHere<OnTriggerEnter2DEvent>()
                .DelHere<TapDownSelfEvent>()
                .DelHere<TapUpSelfEvent>()
                ;
        }
    }
}

