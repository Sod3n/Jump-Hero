using Scellecs.Morpeh.Helpers;
using Scellecs.Morpeh;
using UnityEngine;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ApplyForce))]
public sealed class ApplyForce : SimpleFixedUpdateSystem<ForceTarget, Direction2D> {

    protected override void Process(Entity entity, ref ForceTarget forceTarget, ref Direction2D direction2D, in float deltaTime) 
    {
        Entity target = forceTarget.target;
        ref var rigidbody2DBridge = ref target.GetComponent<Rigidbody2DBridge>();
        rigidbody2DBridge.rigidbody2D.AddForce(forceTarget.power * direction2D.value, ForceMode2D.Force);
    }
}