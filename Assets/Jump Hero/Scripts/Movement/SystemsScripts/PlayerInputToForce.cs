using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInputToForce))]
public sealed class PlayerInputToForce : SimpleUpdateSystem<PlayerActions, ForcePower>
{

    protected override void Process(Entity entity, ref PlayerActions playerInput, ref ForcePower basicForcePower, in float deltaTime)
    {
        var tapPos = playerInput.tapping.action.ReadValue<Vector2>();
        var e = World.CreateEntity();
        ref var forceTarget = ref e.AddComponent<ForceTarget>();
        forceTarget.value = entity;
        ref var forcePower = ref e.AddComponent<ForcePower>();
        forcePower.value = basicForcePower.value;
        ref var dirComp = ref e.AddComponent<Direction2D>();
        dirComp.value = TapPositionToDirection(tapPos);
    }

    private Vector2 TapPositionToDirection(Vector2 tapPosition)
    {
        Vector2 dir;
        switch (tapPosition.x)
        {
            case float x when (x > 0 && x <= 160):
                dir = Vector2.left + Vector2.up * 3;
                dir.Normalize();
                return dir;

            case float x when (x > 160 && x <= 320):
                dir = Vector2.up;
                return dir;

            case float x when (x > 320 && x <= 480):
                dir = Vector2.right + Vector2.up * 3;
                dir.Normalize();
                return dir;

        }
        return Vector2.zero;
    }
}