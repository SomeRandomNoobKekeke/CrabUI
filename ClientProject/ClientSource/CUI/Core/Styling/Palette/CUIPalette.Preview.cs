using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIPalette
  {
    public static void Preview()
    {
      CUIFrame frame = new CUIDefault.Frame()
      {
        Caption = { Text = "Preview" },
      };

      frame["layout"]["header"] = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(false, true),
      };

      frame["layout"]["header"]["bnt1"] = new CUIButton("bruh") { Flex = 1 };
      frame["layout"]["header"]["bnt2"] = new CUIButton("bruh") { Flex = 1 };

      frame["layout"]["main"] = new CUIVerticalList()
      {
        Flex = 1,
      };

      frame["layout"]["main"]["field1"] = new CUIDefault.IntField()
      {
        Key = "bruh",
        Value = 123,
      };

      frame["layout"]["main"]["radios"] = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(false, true),
      };

      frame["layout"]["main"]["radios"]["1"] = new CUIRadioButton("radio 1") { Group = "bruh" };
      frame["layout"]["main"]["radios"]["2"] = new CUIRadioButton("radio 2") { Group = "bruh" };

      frame["layout"]["main"]["checkbox"] = new CUICheckBox()
      {
        Absolute = new CUINullRect(w: 30, h: 30),
      };

      frame.Open();
    }
  }
}