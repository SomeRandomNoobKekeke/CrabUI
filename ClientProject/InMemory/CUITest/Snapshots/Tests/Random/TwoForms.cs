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
        CUIFrame frame = new CUIDefault.Frame("bruh")
        {
          Relative = CUINullRect.One,
        };


        frame["a"] = new CUIDefault.Frame("kekw")
        {
          DeepPalette = CUIPalette.FromColor(Color.Blue),
          Absolute = new CUINullRect(0, 0, 100, 100),
        };

        frame["b"] = new CUIDefault.Frame("lul")
        {
          DeepPalette = CUIPalette.FromColor(Color.Green),
          Absolute = new CUINullRect(0, 0, 100, 100),
        };


        return frame;
      }
    }
  }
}