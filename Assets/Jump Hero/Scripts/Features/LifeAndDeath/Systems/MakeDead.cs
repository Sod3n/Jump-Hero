using Scellecs.Morpeh.Helpers;
using Scellecs.Morpeh;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MakeDead))]
public sealed class MakeDead : SimpleLateUpdateSystem<MakeDeadMarker> 
{
    protected override void Process(Entity entity, ref MakeDeadMarker makeDeadMarker, in float deltaTime) 
    {
        var en = World.CreateEntity();
        en.AddComponent<DeadMarker>();
        //entity.Dispose();
    }
}