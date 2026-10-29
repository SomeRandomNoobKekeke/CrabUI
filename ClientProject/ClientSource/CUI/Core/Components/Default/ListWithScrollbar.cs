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
    public class ListWithScrollbar : CUIHorizontalList
    {
      public CUIVerticalList List { get; }
      public CUIVerticalScrollBar ScrollBar { get; }

      public ListWithScrollbar()
      {
        this["list"] = List = new CUIVerticalList()
        {
          Flex = 1,
          Scrollable = true,
        };

        this["scrollbar"] = ScrollBar = new CUIVerticalScrollBar()
        {
          Absolute = new CUINullRect(w: 24),
          ConnectedList = List,
        };
      }
    }
  }
}