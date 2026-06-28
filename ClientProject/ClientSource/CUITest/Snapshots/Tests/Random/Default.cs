using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
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
        CUIFrame frame = new CUIDefault.Frame()
        {
          Caption = { Text = "bruh" },
        };
        return frame;
      }
    }
  }
}