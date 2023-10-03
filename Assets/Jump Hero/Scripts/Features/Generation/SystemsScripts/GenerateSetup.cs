using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GenerateSetup))]
public sealed class GenerateSetup : UpdateSystem
{
    private Filter _generateSetupMarkers;
    private Filter _setupPrefabs;


    private GameObject _setups;
    public override void OnAwake()
    {
        _setups = new GameObject();
        _setups.name = "Setups";
    }

    public override void OnUpdate(float deltaTime)
    {
        _generateSetupMarkers = this.World.Filter
                                    .With<GenerateSetupMarker>()
                                    .With<Position2D>()
                                    .Build();
        _setupPrefabs = this.World.Filter
                            .With<SetupPrefabs>()
                            .Build();
        foreach(var marker in _generateSetupMarkers)
        {
            ref var stPr = ref _setupPrefabs.First().GetComponent<SetupPrefabs>();
            GameObject obj = Instantiate(stPr.prefabs[0]);
            obj.transform.position = marker.GetComponent<Position2D>().value;
            obj.transform.parent = _setups.transform;


            marker.RemoveComponent<GenerateSetupMarker>();
            marker.AddComponent<Setup>();
        }
        
    }
}