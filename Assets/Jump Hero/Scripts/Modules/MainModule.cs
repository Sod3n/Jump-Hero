using AleVerDes.LeoEcsLiteZoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovementAssembly;
using UtilsAssembly;
using GenerationAssembly;
using CameraFollowAssembly;
using DeathCausesAssembly;
using MovementByPhysicsAssembly;
using MovementByAIAssembly;

internal class MainModule : IEcsModuleInstaller
{
    public IEcsModule Install()
    {
        var module = new EcsModule();

        module
            .Add(new UtilsFeature())
            .Add(new MovementByInputFeature())
            .Add(new MovementByPhysicsFeature())
            .Add(new MovementByAIFeature())
            .Add(new GenerationFeature())
            .Add(new CameraFollowFeature())
            .Add(new DeathCausesFeature())
            ;

        return module;
    }
}
