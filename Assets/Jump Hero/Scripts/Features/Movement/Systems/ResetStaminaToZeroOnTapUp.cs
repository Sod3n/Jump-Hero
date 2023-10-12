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
        EcsFilter _entities;
        EcsPool<TapUpSelfEvent> _tapUpSelfEvents;
        EcsPool<Stamina> _staminas;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _tapUpSelfEvents = _world.GetPool<TapUpSelfEvent>();
            _staminas = _world.GetPool<Stamina>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<TapUpSelfEvent>().Inc<Stamina>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var stamina = ref _staminas.Get(entity);
                stamina.CurrentValue = 0;
            }
        }
    }
}

