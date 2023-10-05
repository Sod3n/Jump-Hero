using Leopotam.EcsLite;

namespace UtilsAssembly
{
    #if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    #endif
    internal class ECSSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {

        }
    }
}