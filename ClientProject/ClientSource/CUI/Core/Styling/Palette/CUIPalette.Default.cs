using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIPalette
  {
    public static CUIPalette Default => new()
    {
      Colors = new()
      {
        ["background"] = Color.Red,
        ["border"] = Color.White,
        ["text"] = Color.White,
      }
    };
  }
}