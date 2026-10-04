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
      //Component LINK:\ClientProject\ClientSource\CUI\Core\Components\Tools\CUIGroupPropView.cs
      public static CUIComponent CUIGroupPropView()
      {
        var frame = new CUIDefault.Frame("CUIGroupPropView", 800, 600);

        CUIComponent wrapper = new();
        wrapper["wrapper2"] = new();
        for (int i = 0; i < 10; i++)
        {
          wrapper["wrapper2"][$"child {i}"] = new CUIComponent();
        }


        frame["layout"]["group"] = new CUIGroupPropView()
        {
          Flex = 1,
          Root = wrapper,
        };

        return frame;
      }
    }
  }
}