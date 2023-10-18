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

    internal class ChangeMomentumOnTapUp : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<Momentum> _momentums;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {

        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<Momentum>().Inc<TapUpSelfEvent>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var momentum = ref _momentums.Get(entity);
                momentum.Value += momentum.IncreaseRate;
                momentum.ResetCurrentTime = momentum.ResetTime;
            }
        }
    }
}

