using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ProvideWorldToMonoSystems))]
public sealed class ProvideWorldToMonoSystems : SimpleUpdateSystem<ProvideWorldToThisSystems>
{

    protected override void Process(Entity entity, ref ProvideWorldToThisSystems provideWorldToThisSystems, in float deltaTime)
    {
        foreach(var system in provideWorldToThisSystems.gameObject.GetComponents<MonoSystem>())
        {
            system.World = World;
        }
        entity.RemoveComponent<ProvideWorldToThisSystems>();
    }
}