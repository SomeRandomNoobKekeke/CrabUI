using System;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using Barotrauma.LuaCs;

namespace CursedUI
{
  public partial class CUIStarterPlugin : IAssemblyPlugin
  {
    static CUIStarterPlugin()
    {
      CUI.Start(); //HACK hopefully this will start CUI before other plugin instances get created so you could create components in property initializers
    }
    public void Initialize()
    {
      // CUI.Start();
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }
    public void Dispose() { }
  }
}