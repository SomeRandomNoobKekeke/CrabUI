using Barotrauma;
using Barotrauma.Plugins;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIStarterPlugin : IBarotraumaPlugin
  {
    // public static IContentPackageRetrievalService ContentPackageRetrievalService
    //   = PluginServiceProvider.GetService<IContentPackageRetrievalService>();
    // public static ContentPackage CUIPackage => ContentPackageRetrievalService.ContentPackage;
    public void DisposeBuildSpecific()
    {
      // ContentPackageRetrievalService = null;
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
