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
    public static partial class Random
    {
      public static CUIComponent CreatePanel(CUIPalette palette)
      {
        var panel = new CUIDefault.HorizontalPanel()
        {
          Absolute = new CUINullRect(h: 100),
        };

        CUITextInput input = new CUITextInput()
        {
          Absolute = new CUINullRect(w: 100, h: 30),
          Anchor = CUIAnchor.LeftCenter,
        };

        panel["text"] = new CUITextBlock("Change Palette color: ");
        panel["wrapper"] = new CUIComponent() { FitContent = new CUIBool2(true, false) };
        panel["wrapper"]["input"] = input;
        panel["button"] = new CUIButton("Update")
        {
          OnMouseDown = (c, e) =>
          {
            Color color = CUICore.Parser.Parse<Color>(input.Text);

            palette.Swap(CUIPalette.FromColor(color));
            CUI.Logger.Log(palette);
          }
        };


        panel.DeepPalette = palette;

        return panel;
      }

      public static CUIComponent MultiPalette()
      {
        CUIFrame frame = new CUIDefault.Frame("MultiPalette")
        {
          Absolute = new CUINullRect(w: 400, h: 600),
        };

        frame["layout"]["panel1"] = CreatePanel(CUICore.Palettes.Primary);
        frame["layout"]["panel2"] = CreatePanel(CUICore.Palettes.Secondary);
        frame["layout"]["panel3"] = CreatePanel(CUICore.Palettes.Tertiary);
        frame["layout"]["panel4"] = CreatePanel(CUICore.Palettes.Quaternary);

        return frame;
      }
    }
  }
}