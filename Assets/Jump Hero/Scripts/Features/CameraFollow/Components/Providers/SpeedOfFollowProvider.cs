using AleVerDes.LeoEcsLiteZoo;

namespace CameraFollowAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class SpeedOfFollowProvider : ConvertComponent<SpeedOfFollow>
    {
    }
}
