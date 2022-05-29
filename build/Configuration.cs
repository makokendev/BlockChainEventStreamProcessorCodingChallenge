using System.Collections.Generic;
using Cake.Common;
using Cake.Common.IO;
using Cake.Common.IO.Paths;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.GitVersion;
using Cake.Core;

namespace CodingChallenge.CakeBuild;
public class Configuration
{
    public Configuration(ICakeContext cakeContext)
    {
        InitVariables();
        SetFolderValues(cakeContext);
        SetDotnetProperties(cakeContext);
    }

    private void InitVariables()
    {
        projectSettingsList = new List<ProjectSettings>();
    }

    public string configuration;
    public DotNetVerbosity? dotNetCoreVerbosity;
    public string dotnetFramework;

    public GitVersion gitVersionResult { get; set; }

    public ConvertableDirectoryPath publishDir;
    public Cake.Core.IO.FilePathCollection solutions;
    public string rootFullPath;
    public string sourceFullPath;
    public string testsFullPath;
    public string publishDirFullPath;
    public string artifactsDirFullPath;

    public List<ProjectSettings> projectSettingsList { get; set; }

    public void SetFolderValues(ICakeContext cakeContext)
    {
        this.publishDir = cakeContext.Directory("../publish/");
        this.solutions = cakeContext.GetFiles("../**/*.sln");
        this.rootFullPath = System.IO.Path.GetFullPath(cakeContext.Directory("../"));
        this.sourceFullPath = System.IO.Path.GetFullPath(cakeContext.Directory("../src"));
        this.testsFullPath = System.IO.Path.GetFullPath(cakeContext.Directory("../tests"));
        this.publishDirFullPath = System.IO.Path.GetFullPath(cakeContext.Directory("../publish"));
        this.artifactsDirFullPath = System.IO.Path.GetFullPath(cakeContext.Directory("../artifacts"));
    }
    private void SetDotnetProperties(ICakeContext cakeContext)
    {
        this.dotNetCoreVerbosity = cakeContext.Argument<DotNetCoreVerbosity>("dotNetCoreVerbosity", DotNetCoreVerbosity.Minimal);
        this.dotnetFramework = cakeContext.Argument<string>("framework", "net6.0");
    }
}

