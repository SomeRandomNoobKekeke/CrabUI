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

namespace CUITests
{
  public partial class SnapshotTests
  {
    public static partial class Components
    {
      //LINK:\ClientProject\ClientSource\CUI\Core\Components\Input\Buttons\CUIIconButton.cs
      public static CUIComponent CUIIconButton()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIIconButton", 400, 600);

        frame["button"] = new CUIIconButton(CUISprite.CrossIcon)
        {
          MasterColor = Color.Lime,
          Anchor = CUIAnchor.Center,
        };

        return frame;
      }
    }
  }
}