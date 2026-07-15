using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Barotrauma;

namespace CrabUI
{
  public partial class CUISetup
  {
    public static CUISetup Default()
    {
      CUISetup setup = new CUISetup();

      setup.DataSources = new GameDataSources();
      setup.Runner = new SoloCUIRunner();
      setup.Core = new CUICore()
      {
        GameScreenRect = new Rectangle(0, 0, GameMain.GraphicsWidth, GameMain.GraphicsHeight)
      };
      setup.WireUp();

      return setup;
    }
  }
}