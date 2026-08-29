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
      public static CUIComponent CUIToggleIconButton()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIToggleIconButton", 400, 600);

        frame["button"] = new CUIToggleIconButton(CUISprite.Cross)
        {
          Anchor = CUIAnchor.Center,
          MasterColor = Color.Red,
          OffIconBlock = { Icon = CUISprite.CheckIcon },
        };

        return frame;
      }
    }
  }
}