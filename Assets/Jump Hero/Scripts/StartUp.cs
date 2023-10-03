using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AleVerDes.LeoEcsLiteZoo;

public class StartUp : MonoBehaviour
{
    [SerializeField] private List<EcsInjectionContext> _injectionContexts;

    private EcsWorld _world;
    private EcsManager _ecsManager;

    private void Awake()
    {
        _world = new EcsWorld();
        ConvertToEntity.DefaultConversionWorld = _world;

        _ecsManager = new EcsManager();
        _ecsManager.SetWorld(_world);

        foreach (var injectionContext in _injectionContexts)
        {
            injectionContext.InitInjector();
            _ecsManager.AddInjector(injectionContext.GetInjector());
        }
    }

    private void Start()
    {
        _ecsManager.InstallModule(new MainEcsModuleInstaller());
    }

    private void Update()
    {
        _ecsManager.Update();
    }

    private void LateUpdate()
    {
        _ecsManager.LateUpdate();
    }

    private void FixedUpdate()
    {
        _ecsManager.FixedUpdate();
    }

    private void OnDestroy()
    {
        _ecsManager.Destroy();
    }
}
