﻿using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;


namespace ReviveCausesAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class ReviveCausesFeature : IEcsUpdateFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new AddReviveBlessingOnTrigger())
                ;
        }
    }
}

