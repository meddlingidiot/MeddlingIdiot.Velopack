using Fallout.Common;
using Fallout.Solutions;
using Automation.Fallout.Components;
using Automation.Fallout.Components.Components;
using Automation.Fallout.Components.DefaultBuilds;
using Automation.Fallout.Components.Parameters;

/// <summary>
/// Build configuration for PackageBuild
/// </summary>
/// Support plugins are available for:
///   - JetBrains ReSharper        https://nuke.build/resharper
///   - JetBrains Rider            https://nuke.build/rider
///   - Microsoft VisualStudio     https://nuke.build/visualstudiowoh
///   - Microsoft VSCode           https://nuke.build/vscode

public class Build : GitHubActionsBuild, IShowVersion, IClean, ICompile, IRestore, IScanForSecrets, IRunUnitTests, IRunIntegrationTests, IGenerateCoverageReport, ITest, IUpdateChangelog, INuGetPublish, ITagRelease, IAnnounceRelease
{

    public static int Main() => Execute<Build>(
        x => ((INuGetPublish)x).PublishNuGet);

    // Was IPackageGitHub. GitHub Packages needs a token even for public packages, which is
    // no use to an app that just wants to reference this and get on with updating itself.
    string? IHasNuGetOrg.NuGetOwner => "themeddlingidiot";

    // The only publish step here, so it is the one that tags.
    bool INuGetPublish.TagsReleasesFromNuGet => true;

    int IHasTests.MinCoverageThreshold => 40;
}
