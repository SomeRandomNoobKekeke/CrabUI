using Barotrauma;
using Microsoft.Xna.Framework;
using Barotrauma.LuaCs;

namespace CursedUI
{
  public partial class CUIStarterPlugin : IAssemblyPlugin
  {
    public void Initialize() => Init();
    public void OnLoadCompleted() { }
    public void PreInitPatching() { }
  }

}

