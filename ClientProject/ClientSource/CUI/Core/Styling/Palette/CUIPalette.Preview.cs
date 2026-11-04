using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIPalette
  {
    private static CUIComponent BlockOfRandomStuff(CUIPalettes palettes, PropertyInfo pi)
    {
      CUIPalette palette = (CUIPalette)pi.GetValue(palettes);

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

        pi.SetValue(palettes, CUIPalette.FromColor(cl));
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


    private static CUIComponent CreatePaletteBlock(CUIPalettes palettes)
    {
      CUIComponent panel = new CUIDefault.HorizontalBackPanel();

      using (new CUIContextStyle<CUIDefault.Panel>(p =>
      {
        p.Flex = 1;
        p.Absolute = new CUINullRect(h: 24);
      }))
      {
        panel.Children.Add(new CUIDefault.Panel() { Palette = palettes.Primary });
        panel.Children.Add(new CUIDefault.Panel() { Palette = palettes.Secondary });
        panel.Children.Add(new CUIDefault.Panel() { Palette = palettes.Tertiary });
        panel.Children.Add(new CUIDefault.Panel() { Palette = palettes.Quaternary });
        panel.Children.Add(new CUIDefault.Panel() { Palette = palettes.Senary });
        panel.Children.Add(new CUIDefault.Panel() { Palette = palettes.Septenary });
        panel.Children.Add(new CUIDefault.Panel() { Palette = palettes.Octonary });
      }

      return panel;

    }

    public static CUIFrame OpenPreview(CUIPalettes palettes = null)
    {
      palettes ??= CUICore.Palettes;

      CUIFrame frame = new CUIDefault.Frame("Palette Preview")
      {
        Absolute = new CUINullRect(w: 600, h: 800),
      };

      frame.Get<CUIVerticalList>("layout").Scrollable = true;

      frame["layout"]["palette"] = CreatePaletteBlock(palettes);

      frame["layout"]["block 1"] = BlockOfRandomStuff(palettes, typeof(CUIPalettes).GetProperty("Primary")!);
      frame["layout"]["block 2"] = BlockOfRandomStuff(palettes, typeof(CUIPalettes).GetProperty("Secondary")!);
      frame["layout"]["block 3"] = BlockOfRandomStuff(palettes, typeof(CUIPalettes).GetProperty("Tertiary")!);
      frame["layout"]["block 4"] = BlockOfRandomStuff(palettes, typeof(CUIPalettes).GetProperty("Quaternary")!);
      frame["layout"]["block 5"] = BlockOfRandomStuff(palettes, typeof(CUIPalettes).GetProperty("Quinary")!);
      frame["layout"]["block 6"] = BlockOfRandomStuff(palettes, typeof(CUIPalettes).GetProperty("Senary")!);
      frame["layout"]["block 7"] = BlockOfRandomStuff(palettes, typeof(CUIPalettes).GetProperty("Septenary")!);
      frame["layout"]["block 8"] = BlockOfRandomStuff(palettes, typeof(CUIPalettes).GetProperty("Octonary")!);

      frame.Open();
      return frame;
    }
  }
}