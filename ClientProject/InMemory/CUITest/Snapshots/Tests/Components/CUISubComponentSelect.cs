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
      //Component LINK:\ClientProject\ClientSource\CUI\Core\Components\Tools\CUISubComponentSelect.cs
      public static CUIComponent CUISubComponentSelect()
      {
        var frame = new CUIDefault.Frame("CUISubComponentSelect", 400, 600);

        CUISubComponentSelect select = new() { Flex = 1 };

        CUIComponent wrapper = new();
        wrapper["wrapper2"] = new();
        for (int i = 0; i < 10; i++)
        {
          wrapper["wrapper2"][$"child {i}"] = new CUIComponent();
        }

        select.Target = wrapper;

        frame["layout"]["select"] = select;

        return frame;
      }
    }
  }
}