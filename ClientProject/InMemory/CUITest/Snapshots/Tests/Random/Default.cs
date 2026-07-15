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
      public static CUIComponent Default()
      {
        CUIFrame frame = new CUIDefault.Frame("bruh")
        {
          Absolute = new CUINullRect(w: 400, h: 600),
        };
        return frame;
      }
    }
  }
}