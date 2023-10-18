using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeathProcessAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class RestartSceneOnKillRequest : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<KillRequest> _killRequests;
        EcsPool<ReviveBlessing> _blessings;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _killRequests = _world.GetPool<KillRequest>();
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<KillRequest>().Exc<ReviveBlessing>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}

