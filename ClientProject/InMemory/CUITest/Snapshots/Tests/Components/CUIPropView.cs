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
    public static partial class Components
    {
      //Component LINK:\ClientProject\ClientSource\CUI\Core\Components\Tools\CUIPropView.cs
      public static CUIComponent CUIPropView()
      {
        var frame = new CUIDefault.Frame("CUIPropView", 400, 600);

        frame.Caption = $"CUIPropView of {frame}";

        frame["layout"]["pages"] = new CUIPages()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          OpenedPage = new CUIPropView()
          {
            Target = frame,
          }
        };

        return frame;
      }
    }
  }
}