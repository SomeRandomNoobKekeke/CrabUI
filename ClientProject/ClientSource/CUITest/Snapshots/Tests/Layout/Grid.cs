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
    public static partial class Layout
    {
      public static CUIComponent Grid()
      {
        CUIFrame frame = new CUIDefault.Frame()
        {
          Caption = { Text = "Grid test" }
        };

        frame["layout"]["grid"] = new CUIGrid()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Background = { Color = Color.Blue },

          RowSizes = new()
          {
            new CUIGridLayout.Line(absolute:30),
            new CUIGridLayout.Line(absolute:30),
            new CUIGridLayout.Line(absolute:30),
          },
          ColumnSizes = new()
          {
            new CUIGridLayout.Line(absolute:30),
            new CUIGridLayout.Line(absolute:30),
            new CUIGridLayout.Line(absolute:30),
          }
        };

        frame["layout"]["grid"]["11"] = new CUITextBlock("11") { GridColumn = 1, GridRow = 1 };
        frame["layout"]["grid"]["12"] = new CUITextBlock("12") { GridColumn = 1, GridRow = 2 };
        frame["layout"]["grid"]["13"] = new CUITextBlock("13") { GridColumn = 1, GridRow = 3 };
        frame["layout"]["grid"]["21"] = new CUITextBlock("21") { GridColumn = 2, GridRow = 1 };
        frame["layout"]["grid"]["22"] = new CUITextBlock("22") { GridColumn = 2, GridRow = 2 };
        frame["layout"]["grid"]["23"] = new CUITextBlock("23") { GridColumn = 2, GridRow = 3 };
        frame["layout"]["grid"]["31"] = new CUITextBlock("31") { GridColumn = 3, GridRow = 1 };
        frame["layout"]["grid"]["32"] = new CUITextBlock("32") { GridColumn = 3, GridRow = 2 };
        frame["layout"]["grid"]["33"] = new CUITextBlock("33") { GridColumn = 3, GridRow = 3 };

        return frame;
      }
    }
  }
}