using System;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using CUILibs;

namespace CursedUI
{
  public partial class CUIStarterPlugin
  {
    static CUIStarterPlugin()
    {
      CUI.Start(); //HACK hopefully this will start CUI before other plugin instances get created so you could create components in property initializers
    }
    public void Init()
    {

    }
    public void Dispose()
    {
      PluginCommands.RemoveCommands();
      DisposeBuildSpecific();
    }
    public void OnContentLoaded() { }
  }
}