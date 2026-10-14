using System;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using Barotrauma.LuaCs;

namespace CursedUI
{
  public partial class CUIStarterPlugin : IAssemblyPlugin
  {
    public void Initialize()
    {
      CUI.Start();
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }
    public void Dispose() { }
  }
}