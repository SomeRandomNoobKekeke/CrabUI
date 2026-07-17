using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Layout
    {
      public static CUIComponent DoubleFitContent()
      {
        CUIFrame frame = new CUIDefault.Frame()
        {
          Caption = { Text = "DoubleFitContent" },
        };

        frame["layout"]["list1"] = new CUIVerticalList()
        {
          FitContent = new(false, true),
          Background = { Color = new Color(0, 0, 255) },
        };

        frame["layout"]["list1"]["text1"] = new CUITextBlock("bruh1");
        frame["layout"]["list1"]["text2"] = new CUITextBlock("bruh2");

        return frame;
      }
    }
  }
}