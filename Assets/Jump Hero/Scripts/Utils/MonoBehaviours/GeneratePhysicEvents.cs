using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System.Collections;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]

public class GeneratePhysicEvents : MonoBehaviour
{
    private World _world;
    [SerializeField] private Installer _installer;

    private void Awake()
    {
        _world = _installer.World;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var collisionStay2DEvent = _world.GetEvent<CollisionStay2DEvent>();
        collisionStay2DEvent.NextFrame(new CollisionStay2DEvent());
    }
}
