using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;
using UtilsAssembly;

namespace MovementAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class ResetStaminaToZeroOnTapUp : IEcsRunSystem
    {
        EcsQuery<TapUpSelfEvent, Stamina> _entities;
        EcsPool<TapUpSelfEvent> _tapUpSelfEvents;
        EcsPool<Stamina> _staminas;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var stamina = ref _staminas.Get(entity);
                stamina.CurrentValue = 0;
            }
        }
    }
}

