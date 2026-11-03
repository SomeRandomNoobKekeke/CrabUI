using System;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using CUILibs;
using System.Runtime.CompilerServices;

namespace CursedUI
{
  public partial class CUIStarterPlugin
  {
    static CUIStarterPlugin()
    {
      CUI.Start(); //HACK hopefully this will start CUI before other plugin instances get created so you could create components in property initializers
    }
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Init()
    {

    }
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Dispose()
    {
      PluginCommands.RemoveCommands();
      PluginLifeCycle.RaiseStop();
      DisposeBuildSpecific();
    }
    public void OnContentLoaded() { }
  }
}