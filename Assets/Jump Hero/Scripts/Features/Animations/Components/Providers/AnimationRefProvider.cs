using AleVerDes.LeoEcsLiteZoo;

namespace AnimationsAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class AnimationRefProvider : ConvertComponent<AnimationRef>
    {
    }
}
