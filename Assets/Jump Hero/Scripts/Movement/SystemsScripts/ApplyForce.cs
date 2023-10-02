using Scellecs.Morpeh.Helpers;
using Scellecs.Morpeh;
using UnityEngine;
using log4net.Util;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ApplyForce))]
public sealed class ApplyForce : SimpleFixedUpdateSystem<ForceTarget, ForcePower, Direction2D> {

    protected override void Process(Entity entity, ref ForceTarget forceTarget, ref ForcePower forcePower, ref Direction2D direction2D, in float deltaTime) 
    {
        Entity target = forceTarget.value;
        ref var rigidbody2DBridge = ref target.GetComponent<Rigidbody2DBridge>();
        rigidbody2DBridge.rigidbody2D.AddForce(forcePower.value * direction2D.value, ForceMode2D.Force);
        World.RemoveEntity(entity);
    }

    
}