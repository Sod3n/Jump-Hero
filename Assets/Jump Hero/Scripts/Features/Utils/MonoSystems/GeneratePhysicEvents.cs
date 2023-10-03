using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System.Collections;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]

public class GeneratePhysicEvents : MonoSystem
{
    [SerializeField] private EntityProvider entityProvider;
    private void Awake()
    {
        Entity e = (entityProvider?.Entity) ?? GetComponent<EntityProvider>()?.Entity;
        Debug.Log(e);
        if (e != null)
        {
            ref var p = ref e.AddComponent<ProvideWorldToThisSystems>();
            
            p.gameObject = gameObject;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (World != null)
        {
            var collisionStay2DEvent = World.GetEvent<CollisionStay2DEvent>();
            collisionStay2DEvent.NextFrame(new CollisionStay2DEvent());
            Debug.Log("Collision");
        }
    }
}
