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
        var frame = new CUIDefault.Frame("CUIGroupPropView", 400, 600);


        frame["layout"]["group"] = new CUIGroupPropView()
        {
          Flex = 1,
        };

        return frame;
      }
    }
  }
}