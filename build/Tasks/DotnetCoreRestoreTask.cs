using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Restore;
using Cake.Frosting;

namespace CodingChallenge.CakeBuild.Tasks;
[TaskName("DotnetCore-Restore-Task")]
public sealed class DotnetCoreRestoreTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var settings = new DotNetRestoreSettings
        {
            DisableParallel = false,
            NoCache = true,
            Verbosity = context.Config.dotNetCoreVerbosity
        };

        foreach (var solution in context.Config.solutions)
        {
            context.Information("Restoring NuGet packages for '{0}'...", solution);
            context.DotNetRestore(solution.FullPath, settings);
            context.Information("NuGet packages restored for '{0}'.", solution);
        }
    }
}