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
      public static CUIComponent CUIRangeInput()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIRangeInput", 400, 600);

        CUIRangeInput rangeInput = new CUIRangeInput()
        {
          Anchor = CUIAnchor.Center,
          Relative = new CUINullRect(w: 0.8f),
          Absolute = new CUINullRect(h: 50),
          PinCount = 5,
          OnChanged = (l) => CUI.Logger.Log(l),

          // Pin = 3,
        };

        frame["CUIRangeInput"] = rangeInput;


        CUITextBlock label = new CUITextBlock("bruh")
        {
          Anchor = CUIAnchor.Center,
          Scale = 0.9f,
          TextColor = new Color(64, 0, 0),
        };

        rangeInput.Handle["label"] = label;
        rangeInput.HandleDragged += (l) => label.Text = Math.Round(l, 2).ToString();
        rangeInput.Changed += (l) => label.Text = Math.Round(l, 2).ToString();

        for (int i = 0; i < rangeInput.Pins.Children.Count; i++)
        {
          rangeInput.Pins[i]["label"] = new CUITextBlock($"{i}")
          {
            Anchor = CUIAnchor.CenterBottom,
            ParentAnchor = CUIAnchor.CenterTop,
          };
        }

        return frame;
      }
    }
  }
}