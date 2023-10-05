using AleVerDes.LeoEcsLiteZoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovementAssembly;
using UtilsAssembly;
using GenerationAssembly;

internal class MainModule : IEcsModuleInstaller
{
    public IEcsModule Install()
    {
        var module = new EcsModule();

        module
            .AddFeature(new UtilsFeature())
            .AddFeature(new MovementFeature())
            .AddFeature(new GenerationFeature())
            ;

        return module;
    }
}
