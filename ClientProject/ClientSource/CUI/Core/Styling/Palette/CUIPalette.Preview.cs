using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIPalette
  {
    private static CUIComponent BlockOfRandomStuff(CUIPalette palette)
    {
      CUIHorizontalList wrapper = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(false, true),
        // Margin = new CUISizes(bottom: 20),
      };

      CUIVerticalList block = new CUIVerticalList()
      {
        Flex = 1,
        FitContent = new CUIBool2(false, true),
      };

      wrapper["color select"] = new CUIColorPicker()
      {
        Absolute = new CUINullRect(w: 390, h: 360),
        HueSelectWidth = 30,
        OnSelected = (cl) =>
        {
          float alpha = block.Get<CUIRangeInput>("alphainput").Lambda;
          cl = new Color(cl.R, cl.G, cl.B, (int)Math.Round(alpha * 255));

          block.Get<CUITextInput>("colorinput").Text = CUICore.Parser.Serialize(cl);
        },
      };

      wrapper["block of random stuff"] = block;


      block["button"] = new CUIButton("button");


      void UpdatePalette()
      {

        Color cl = CUICore.Parser.Parse<Color>(block.Get<CUITextInput>("colorinput").Text);
        float alpha = block.Get<CUIRangeInput>("alphainput").Lambda;

        cl = new Color(cl.R, cl.G, cl.B, (int)Math.Round(alpha * 255));

        palette.Swap(CUIPalette.FromColor(cl));
      }

      block["alphainput"] = new CUIRangeInput()
      {
        Absolute = new CUINullRect(h: 24),
        OnChanged = (l) => UpdatePalette(),
        Lambda = 1,
      };
      block["colorinput"] = new CUITextInput()
      {
        Anchor = CUIAnchor.Center,
        Absolute = new CUINullRect(h: 24),
        Text = CUICore.Parser.Serialize(palette.BaseColor),
        OnInput = (s) => UpdatePalette(),
      };

      block["controls"] = new CUIDefault.VerticalPanel()
      {
        FitContent = new CUIBool2(false, true),
      };

      block["controls"]["radios"] = new CUIDefault.HorizontalPanel();

      block["controls"]["radios"]["1"] = new CUIRadioButton("radio button 1") { GroupName = "bruh" };
      block["controls"]["radios"]["2"] = new CUIRadioButton("radio button 2") { GroupName = "bruh" };

      block["controls"]["toggle button"] = new CUIToggleButton("CUIToggleButton");

      block["controls"]["checkbox"] = new CUICheckBox()
      {
        Absolute = new CUINullRect(w: 24, h: 24),
      };

      block["panel"] = new CUIDefault.HorizontalPanel();
      block["panel"]["text"] = new CUITextBlock("panel");

      block["backpanel"] = new CUIDefault.HorizontalBackPanel();
      block["backpanel"]["text"] = new CUITextBlock("backpanel");

      block["backpanel"] = new CUIDefault.HorizontalBackPanel();
      block["backpanel"]["text"] = new CUITextBlock("backpanel");

      block.DeepPalette = palette;

      return wrapper;
    }


    public static void Preview()
    {
      CUIFrame frame = new CUIDefault.Frame("Palette Preview")
      {
        Absolute = new CUINullRect(w: 600, h: 800),
      };

      frame.Get<CUIVerticalList>("layout").Scrollable = true;

      frame["layout"]["block 1"] = BlockOfRandomStuff(CUICore.Palettes.Primary);
      frame["layout"]["block 2"] = BlockOfRandomStuff(CUICore.Palettes.Secondary);
      frame["layout"]["block 3"] = BlockOfRandomStuff(CUICore.Palettes.Tertiary);
      frame["layout"]["block 4"] = BlockOfRandomStuff(CUICore.Palettes.Quaternary);
      frame["layout"]["block 5"] = BlockOfRandomStuff(CUICore.Palettes.Quinary);
      frame["layout"]["block 6"] = BlockOfRandomStuff(CUICore.Palettes.Senary);
      frame["layout"]["block 7"] = BlockOfRandomStuff(CUICore.Palettes.Septenary);
      frame["layout"]["block 8"] = BlockOfRandomStuff(CUICore.Palettes.Octonary);

      frame.Open();
    }
  }
}