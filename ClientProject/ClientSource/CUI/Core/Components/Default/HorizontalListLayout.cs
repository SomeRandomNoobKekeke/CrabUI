using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CursedUI
{
  public static partial class CUIDefault
  {
    public class HorizontalListLayout : CUIHorizontalList
    {
      protected override void InitStyle()
      {
        base.InitStyle();
        Relative = new CUINullRect(0, 0, 1, 1);
        Flex = 1;
      }
    }
  }
}