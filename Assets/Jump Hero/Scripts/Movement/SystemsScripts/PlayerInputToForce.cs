using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInputToForce))]
public sealed class PlayerInputToForce : SimpleUpdateSystem<PlayerInput>
{

    protected override void Process(Entity entity, ref PlayerInput playerInput, in float deltaTime)
    {
        var tap = playerInput.tapping.action.ReadValue<Vector2>();
        Debug.Log(tap);
    }
}