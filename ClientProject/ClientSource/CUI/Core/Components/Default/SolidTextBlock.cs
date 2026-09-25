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
    public class SolidTextBlock : CUITextBlock
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<SolidTextBlock>((c) =>
      {
        c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.4f);
      });
      public SolidTextBlock() : base() { }
      public SolidTextBlock(string text) : base(text) { }
    }
  }
}