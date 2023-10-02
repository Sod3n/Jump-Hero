using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ApplyLerpFollow))]
public sealed class ApplyLerpFollow : SimpleFixedUpdateSystem<TargetForFollow, Position2D>
{

    protected override void Process(Entity entity, ref TargetForFollow targetForFollow, ref Position2D position2D, in float deltaTime)
    {
       
        ref Position2D targetPos = ref targetForFollow.target.GetComponent<Position2D>();
        position2D.value = Vector2.Lerp(position2D.value, targetPos.value, deltaTime);
        Debug.Log(targetPos.value);
    }
}