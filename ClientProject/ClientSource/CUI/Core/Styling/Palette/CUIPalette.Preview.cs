using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIPalette
  {
    private static CUIComponent BlockOfRandomStuff(CUIPalette palette)
    {
      CUIComponent block = new CUIVerticalList()
      {
        Relative = new CUINullRect(w: 1),
        Absolute = new CUINullRect(h: 150),
        // FitContent = new CUIBool2(false, true),
      };

      block["header"] = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(false, true),
      };

      block["header"]["bnt1"] = new CUIButton("button") { Flex = 1 };
      block["header"]["bnt2"] = new CUIButton("button") { Flex = 1 };

      block["main"] = new CUIVerticalList()
      {
        Flex = 1,
      };

      block["main"]["input"] = new CUITextInput()
      {
        Anchor = CUIAnchor.Center,
        Absolute = new CUINullRect(h: 24),
        Text = CUICore.Parser.Serialize(palette.BaseColor),
        OnInput = (s) => palette.Swap(CUIPalette.FromColor(CUICore.Parser.Parse<Color>(s))),
      };

      block["main"]["panel"] = new CUIDefault.HorizontalPanel()
      {
        FitContent = new CUIBool2(false, true),
      };

      block["main"]["panel"]["radios"] = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(false, true),
      };

      block["main"]["panel"]["radios"]["1"] = new CUIRadioButton("radio button 1") { Group = "bruh" };
      block["main"]["panel"]["radios"]["2"] = new CUIRadioButton("radio button 2") { Group = "bruh" };

      block["main"]["panel"]["checkbox"] = new CUICheckBox()
      {
        Absolute = new CUINullRect(w: 30, h: 30),
      };

      block.DeepPalette = palette;

      return block;
    }


    public static void Preview()
    {
      CUIFrame frame = new CUIDefault.Frame("Palette Preview")
      {
        Absolute = new CUINullRect(w: 400, h: 600),
      };

      frame["layout"]["block 1"] = BlockOfRandomStuff(CUICore.Palettes.Primary);
      frame["layout"]["block 2"] = BlockOfRandomStuff(CUICore.Palettes.Secondary);
      frame["layout"]["block 3"] = BlockOfRandomStuff(CUICore.Palettes.Tertiary);
      frame["layout"]["block 4"] = BlockOfRandomStuff(CUICore.Palettes.Quaternary);

      frame.Open();
    }
  }
}