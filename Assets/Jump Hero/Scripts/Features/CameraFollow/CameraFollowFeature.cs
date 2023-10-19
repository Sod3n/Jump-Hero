﻿using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using MovementAssembly;

namespace CameraFollowAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class CameraFollowFeature : IEcsUpdateFeature
    {
        public void SetupUpdateSystems(IEcsSystems systems)
        {
            systems
                .Add(new FollowTransformsWithLerp())
                //.Add(new ConvertMaxHeightOfFollowedToRestrictions())
                .Add(new LerpSpeedOfFollow())
                //.Add(new ChangeFollowSpeedByRestrictions())
                ;
        }
    }
}

