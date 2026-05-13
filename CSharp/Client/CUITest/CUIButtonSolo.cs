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
  public static partial class CUITestFactories
  {
    public static CUIComponent CUIButtonSolo()
    {
      return new CUIButton()
      {
        Absolute = new CUINullRect(1000, 300, 200, 100),
        BackgroundColor = Color.White,
      };
    }
  }
}