using Cake.Frosting;
using CodingChallenge.CakeBuild;
using CodingChallenge.CakeBuild.Tasks;

public static class Extensions
{
    public static CakeHost InstallBaseTools(this CakeHost cakehost)
    {
        return cakehost.
            InstallTool(new System.Uri("dotnet:?package=GitVersion.Tool&version=5.8.1"))
            .InstallTool(new System.Uri("nuget:?package=nuget.commandline&version=5.3.0"));
    }
}
public static class Program
{
    public static void SetEnvironmentVariables()
    {

    }
    public static int Main(string[] args)
    {
        SetEnvironmentVariables();
        return new CakeHost()
            .UseContext<BuildContext>()
            //.AddAssembly(typeof(BuildContext).Assembly)
            .InstallBaseTools()
            .Run(args);
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(CommonCleanTask))]
[IsDependentOn(typeof(SetupTask))]
[IsDependentOn(typeof(GitVersionTask))]
[IsDependentOn(typeof(DotnetCoreCleanTask))]
[IsDependentOn(typeof(DotnetCoreRestoreTask))]
[IsDependentOn(typeof(DotnetCoreBuildTask))]
[IsDependentOn(typeof(DotnetCoreUnitTestTask))]
[IsDependentOn(typeof(DotnetCorePublishTask))]
[IsDependentOn(typeof(RunConsoleAppCommandsTask))]
public class DefaultTask : FrostingTask
{
}
