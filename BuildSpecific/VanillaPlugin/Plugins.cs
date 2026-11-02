using Barotrauma;
using Barotrauma.Plugins;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIStarterPlugin : IBarotraumaPlugin
  {
    // public ContentPackage _CUIPackage
    //   = PluginServiceProvider.GetService<IContentPackageRetrievalService>().ContentPackage;


    public static ContentPackage CUIPackage => ContentPackageManager.EnabledPackages.All.First(
      package => package.Name == "CUI"
    );
    public void DisposeBuildSpecific()
    {

    }
  }
}


namespace CUITests
{
  public partial class CUITestPlugin : IBarotraumaPlugin
  {
    public void DisposeBuildSpecific() { }
  }
}
