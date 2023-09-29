using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SynchronizeTransformAndPosition2D))]
public sealed class SynchronizeTransformAndPosition2D : SimpleUpdateSystem<TranformBridge, Position2D>
{

    protected override void Process(Entity entity, ref TranformBridge tranformBridge, ref Position2D position2D, in float deltaTime)
    {
        tranformBridge.transform.position = position2D.value;
    }
}