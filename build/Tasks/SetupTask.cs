using Cake.Frosting;
namespace CodingChallenge.CakeBuild.Tasks;
[TaskName("Setup-Task")]
public sealed class SetupTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        SetupDeployProjects(context);
    }

    private static void SetupDeployProjects(BuildContext context)
    {
        context.Config.projectSettingsList.Add(new ProjectSettings()
        {
            ProjectName = "CodingChallenge.Console",
            PublishProject = true
        });
    }
}