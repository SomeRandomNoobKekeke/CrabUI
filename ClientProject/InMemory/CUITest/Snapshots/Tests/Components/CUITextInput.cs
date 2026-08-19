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
    public static partial class Components
    {
      public static CUIComponent CUITextInput()
      {

        CUIFrame frame = new()
        {
          Background = { Color = new Color(0, 0, 64) },
          Absolute = new CUINullRect(0, 0, 300, 400),
          Anchor = new Vector2(0.7f, 0.5f),
          Resizable = true,
        };

        frame["textinput"] = new CUITextInput()
        {
          Absolute = new CUINullRect(0, 0, 100, 30),
          Text = "bebebe",
        };

        return frame;
      }
    }
  }
}