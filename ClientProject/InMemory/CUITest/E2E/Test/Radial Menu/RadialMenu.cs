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
using System.IO;

namespace CursedUIUser
{
  public partial class E2ETestPack
  {
    public partial class RadialMenu : IE2ETest
    {

      public RadialMenuUI Menu { get; private set; }

      public CUIButton OpenButton { get; private set; }

      public void Initialize()
      {
        Menu = new();

        OpenButton = new("Open Radial Menu")
        {
          Anchor = new Vector2(0, 0.4f),
          MasterColor = Color.Pink,
          OnMouseDown = (e) => CUI.Main["radial menu"] = Menu,
        };


        CUI.Main["open"] = OpenButton;

        OpenButton.Click();
      }

      public void Dispose()
      {
        OpenButton.RemoveSelf();
        Menu.RemoveSelf();
      }
    }
  }
}