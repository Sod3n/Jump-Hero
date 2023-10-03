using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SynchronizePosition2DToTransform))]
public sealed class SynchronizePosition2DToTransform : SimpleUpdateSystem<TransformBridge, Position2D>
{

    protected override void Process(Entity entity, ref TransformBridge transformBridge, ref Position2D position2D, in float deltaTime)
    {
        if(!entity.Has<Rigidbody2DBridge>())
            transformBridge.transform.position = new Vector3(position2D.value.x, position2D.value.y, transformBridge.transform.position.z);
    }

}