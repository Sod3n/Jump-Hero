﻿using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;


namespace MovementByAIAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class MovementByAIFeature : IEcsFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            
        }

        public void SetupLateUpdateSystems(IEcsSystems systems)
        {
        }

        public void SetupFixedUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new MoveBetweenPatrolPoints())
                ;
        }

        public void SetupInjector(IEcsInjector injector)
        {

        }
    }
}

