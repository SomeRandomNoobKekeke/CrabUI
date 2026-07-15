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
    public static partial class Random
    {
      public static CUIComponent TwoForms()
      {
        CUIFrame frame = new CUIDefault.Frame()
        {
          Caption = { Text = "bruh" },
          Relative = CUINullRect.One,
        };


        frame["a"] = new CUIDefault.Frame()
        {
          Caption = { Text = "kekw" },
          DeepPalette = CUIPalette.Blue,
          Absolute = new CUINullRect(0, 0, 100, 100),
        };

        frame["b"] = new CUIDefault.Frame()
        {
          Caption = { Text = "lul" },
          DeepPalette = CUIPalette.Green,
          Absolute = new CUINullRect(0, 0, 100, 100),
        };


        return frame;
      }
    }
  }
}