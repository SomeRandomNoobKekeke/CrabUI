using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Components
    {
      public static CUIComponent CUIIconButton()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIIconButton", 400, 600);

        frame["button"] = new CUIIconButton(CUISprite.CrossIcon with { Color = Color.Lime })
        {
          Anchor = CUIAnchor.Center,
        };

        return frame;
      }
    }
  }
}