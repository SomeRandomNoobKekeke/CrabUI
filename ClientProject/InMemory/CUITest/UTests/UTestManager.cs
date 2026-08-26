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
  public class UTestManager : CUIPage
  {
    public ILogger Logger => CUI.Logger;
    public CUIVerticalList ButtonList { get; set; }

    public void Run(string name)
    {
      DebugConsole.ExecuteCommand($"utest {name}");
    }

    public void Refresh()
    {
      ButtonList.Children.Clear();
      foreach (string name in UTestExplorer.TestNames)
      {
        ButtonList.Add(new CUIButton()
        {
          Text = name,
          TextAnchor = CUIAnchor.LeftCenter,
          Absolute = new CUINullRect(h: 30),
          OnMouseDown = (e) => Run(name),
          Palette = CUICore.Palettes.Tertiary,
        });
      }
    }

    private void CreateUI()
    {
      OnOpen.Add(Refresh);

      Background.Color = new Color(32, 32, 32);

      this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };
      this["layout"]["btnlist"] = ButtonList = new CUIVerticalList()
      {
        Flex = 1,
        Scrollable = true,
      };
    }

    public UTestManager()
    {
      CreateUI();
    }



  }
}