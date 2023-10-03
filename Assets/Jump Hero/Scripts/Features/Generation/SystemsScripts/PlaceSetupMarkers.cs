using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlaceSetupMarkers))]
public sealed class PlaceSetupMarkers : UpdateSystem 
{
    private Filter _generateCenterFilter;
    private Filter _setupsFilter;
    private Filter _setupSizeFilter;
    public override void OnAwake() 
    {
        
    }

    public override void OnUpdate(float deltaTime) 
    {
        _generateCenterFilter = this.World.Filter
                                .With<GenerateCenterMarker>()
                                .With<Position2D>()
                                .With<Size>()
                                .Build();
        _setupsFilter = this.World.Filter
                            .With<Setup>()
                            .With<Position2D>()
                            .Build();
        _setupSizeFilter = this.World.Filter
                            .With<Size>()
                            .With<SetupSizeMarker>()
                            .Build();


        foreach (var entity in _generateCenterFilter)
        {
            ref var genPosition = ref entity.GetComponent<Position2D>();
            ref var genSize = ref entity.GetComponent<Size>();

            ref var setupSize = ref _setupSizeFilter.First().GetComponent<Size>();

            float xStart = genPosition.value.x - genSize.value.x / 2;
            float yStart = genPosition.value.y - genSize.value.y / 2;
            float xEnd = genPosition.value.x + genSize.value.x / 2;
            float yEnd = genPosition.value.y + genSize.value.y / 2;
            for (float x = xStart; x <= xEnd; x += setupSize.value.x)
            {
                for(float y = yStart; y <= yEnd; y += setupSize.value.y)
                {
                    Vector2 setupStartPoint = GetSetupStartPoint(new Vector2(x, y), setupSize.value);
                    if (!SetupExist(setupStartPoint))
                    {
                        var newEnt = World.CreateEntity();
                        ref var pos = ref newEnt.AddComponent<Position2D>();
                        pos.value = setupStartPoint;

                        newEnt.AddComponent<GenerateSetupMarker>();
                        
                    }
                    
                }
            }
        }
    }

    private bool SetupExist(Vector2 atPoint)
    {
        bool exist = false;
        foreach (var entity in _setupsFilter)
        {
            ref var setPosition = ref entity.GetComponent<Position2D>();
            if(setPosition.value == atPoint) exist = true;
        }
        return exist;
    }
    private Vector2 GetSetupStartPoint(Vector2 insidePosition, Vector2Int setupSize) //we also think that start point at zero coordinates
    {
        float x = (insidePosition.x / setupSize.x);
        float y = (insidePosition.y / setupSize.y);

        x = Mathf.Floor(x);
        y = Mathf.Floor(y);

        x = x * setupSize.x;
        y = y * setupSize.y;

        return new Vector2(x, y);
    }
}