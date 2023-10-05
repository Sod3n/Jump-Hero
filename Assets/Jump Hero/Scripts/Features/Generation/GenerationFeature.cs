﻿using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;


namespace GenerationAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class GenerationFeature : IEcsFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new CreateGenerationRectangle())
                .Add(new PlaceGenerateSetupMarkers())
                .Add(new GenerateSetups())
                .Add(new SetNoiseSeed())
                .Add(new ActivateSubElements())
                .DelHere<ChanceOfActivationSubElement>()   
                .DelHere<GenerateSetupMarker>()
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

