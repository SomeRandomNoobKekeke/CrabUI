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
    public static partial class Layout
    {
      public static CUIComponent FitContentToggle()
      {
        CUIFrame frame = new CUIDefault.Frame("FitContentToggle", 400, 600);


        frame["layout"]["wrapper"] = new CUIVerticalList() { FitContent = new CUIBool2(false, true) };
        frame["layout"]["wrapper"]["toggle"] = new CUIToggleButton("toggle")
        {
          OnToggle = (state) => frame["layout"]["wrapper"]["main"].FitContent = new CUIBool2(false, state),
          // OnToggle = (state) => frame["layout"]["wrapper"]["main"].Absolute = frame["layout"]["wrapper"]["main"].Absolute with
          // {
          //   Height = state ? 100 : null,
          // },
        };
        frame["layout"]["wrapper"]["main"] = new CUIVerticalList()
        {
          FitContent = new CUIBool2(false, false)
        };

        frame["layout"]["wrapper"]["main"]["content"] = new CUIButton("content")
        {
          Absolute = new CUINullRect(h: 100),
        };

        return frame;
      }
    }
  }
}