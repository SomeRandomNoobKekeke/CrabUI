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
    public static partial class Layout
    {
      public static CUIComponent FitComponents()
      {
        CUIFrame frame = new CUIDefault.Frame("FitComponents", 600, 800);

        CUIComponent CreateBlock1()
        {
          CUIVerticalList block = new CUIVerticalList()
          {
            FitContent = new CUIBool2(true, true),
            Absolute = new CUINullRect(x: 20, y: 50),
          };

          block.Add(new CUITextBlock("CUITextBlock"));
          block.Add(new CUIComponent() { Absolute = new CUINullRect(w: 20, h: 20) });
          block.Add(new CUIButton("CUIButton"));
          block.Add(new CUIRadioButton("CUIRadioButton"));
          block.Add(new CUIToggleButton("CUIToggleButton"));
          block.Add(new CUITextInput() { Text = "CUITextInput" });

          block.Palette = CUICore.Palettes.Secondary;

          return block;
        }

        using (new CUIContextStyle<CUIComponent>(c => c.Background.Color = Color.Cyan))
        using (new CUIContextStyle<CUITextBlock>(c => c.Background.Color = Color.Green))
        using (new CUIContextStyle<CUIVerticalList>(c => c.Background.Color = Color.Orange))
        using (new CUIContextStyle<CUIHorizontalList>(c => c.Background.Color = Color.Pink))
        {
          frame["block 1"] = CreateBlock1();
        }

        return frame;
      }
    }
  }
}