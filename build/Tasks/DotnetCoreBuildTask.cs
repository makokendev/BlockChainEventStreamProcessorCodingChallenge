using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Core;
using Cake.Frosting;
namespace CodingChallenge.CakeBuild.Tasks;
[TaskName("DotnetCore-Build-Task")]
public sealed class DotnetCoreBuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var msBuildSettings = new DotNetCoreMSBuildSettings
        {
            Verbosity = context.Config.dotNetCoreVerbosity
        };

        var settings = new DotNetBuildSettings
        {
            Configuration = context.Config.configuration,
            MSBuildSettings = msBuildSettings,
            NoRestore = true,
            ArgumentCustomization = args => args.Append($"-p:Version={context.Config.gitVersionResult.NuGetVersionV2}")
        };

        if (!string.IsNullOrEmpty(context.Config.dotnetFramework))
        {
            context.Information($"dotnet build - framework set as {context.Config.dotnetFramework}.");
            settings.Framework = context.Config.dotnetFramework;
        }
        else
        {
            context.Information($"dotnet build  - Using default framework.");
        }

        foreach (var solution in context.Config.solutions)
        {
            context.Information("Building '{0}'...", solution);
            context.DotNetBuild(solution.FullPath, settings);
            context.Information("'{0}' has been built.", solution);
        }
    }
}