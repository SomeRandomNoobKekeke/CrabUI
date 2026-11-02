using System;
using Barotrauma;
using Microsoft.Xna.Framework;
using Barotrauma.LuaCs;
using System.Linq;

namespace CursedUI
{
  public partial class CUIStarterPlugin : IAssemblyPlugin
  {
    public void Initialize()
    {
      Init();
    }
    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void DisposeBuildSpecific()
    {

    }
  }
}

namespace CUITests
{
  public partial class CUITestPlugin : IAssemblyPlugin
  {
    public void Initialize() => Init();
    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void DisposeBuildSpecific()
    {

    }
  }
}

