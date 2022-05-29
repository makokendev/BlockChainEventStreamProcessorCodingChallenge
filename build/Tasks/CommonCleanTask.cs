using Cake.Common.IO;
using Cake.Frosting;

namespace CodingChallenge.CakeBuild.Tasks;
[TaskName("Common-Clean-Task")]
public sealed class CommonCleanTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CleanDirectories($"{context.Config.sourceFullPath}/**/bin/{context.Config.configuration}");
        context.CleanDirectories($"{context.Config.sourceFullPath}/**/obj/{context.Config.configuration}");
        context.CleanDirectories($"{context.Config.testsFullPath}/**/bin/{context.Config.configuration}");
        context.CleanDirectories($"{context.Config.testsFullPath}/**/obj/{context.Config.configuration}");
        context.CleanDirectory(context.Config.artifactsDirFullPath);
        context.CleanDirectory(context.Config.publishDir);
    }
}