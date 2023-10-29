using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;

namespace ChestAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class SpawnContentOnChestOpenned : IEcsRunSystem
    {
        EcsQuery<ChestOpennedSelfEvent, ChestContents> _entities;
        EcsPool<ChestContents> _chestContents;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var chestContents = ref _chestContents.Get(entity);

                foreach(var convertToEntity in chestContents.ConvertToEntities)
                {
                    convertToEntity.gameObject.SetActive(true);
                    convertToEntity.Convert();
                    
                }
            }
        }
    }
}

