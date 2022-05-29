using Cake.Core;
using Cake.Frosting;

namespace CodingChallenge.CakeBuild;

public class BuildContext : FrostingContext
{
    public ICakeContext _context { get; set; }
    public Configuration Config { get; set; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        _context = context;
        Config = new Configuration(context);
    }

    public string GetProjectFilePathUsingBuiltinArguments(string projectName)
    {
        return $"{this.Config.sourceFullPath}/{projectName}/{projectName}.csproj";
    }
}
