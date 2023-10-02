using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(UpdateOnGround))]
public sealed class UpdateOnGround : SimpleFixedUpdateSystem<OnGround, Collider2DBridge>
{

    protected override void Process(Entity entity, ref OnGround onGround, ref Collider2DBridge collider2DBridge, in float deltaTime)
    {
        
    }
}