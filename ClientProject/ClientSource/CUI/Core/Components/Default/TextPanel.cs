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
    public class TextPanel : CUITextBlock
    {
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<TextPanel>((c) =>
      {
        c.Background.Color = c.Palette["panel"];
      });

      public TextPanel() : base() { }
      public TextPanel(string text) : base(text) { }
    }


  }
}