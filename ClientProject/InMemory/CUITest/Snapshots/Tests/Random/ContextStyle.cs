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
    public static partial class Random
    {
      public static CUIComponent ContextStyle()
      {
        CUIFrame frame = new CUIDefault.Frame("ContextStyle", 400, 600);

        frame["layout"]["button 1"] = new CUIButton("button 1");

        using (new CUIContextStyle<CUIButton>((c) => c.MasterColor = Color.Orange))
        {
          frame["layout"]["button 2"] = new CUIButton("button 2");
          frame["layout"]["button 3"] = new CUIButton("button 3");
          frame["layout"]["button 4"] = new CUIButton("button 4");
        }

        frame["layout"]["button 5"] = new CUIButton("button 5");

        return frame;
      }
    }
  }
}