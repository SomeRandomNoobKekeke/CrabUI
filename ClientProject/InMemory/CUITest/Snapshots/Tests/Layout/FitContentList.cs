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
      public static CUIComponent FitContentList()
      {
        CUIFrame frame = new CUIDefault.Frame("FitContentList", 600, 800);

        CUIComponent CreateBlock1()
        {
          CUIVerticalList block = new CUIVerticalList()
          {
            FitContent = new CUIBool2(true, true),
            Absolute = new CUINullRect(x: 20, y: 50),
          };

          block.Add(new CUITextBlock("123"));
          block.Add(new CUIComponent() { Absolute = new CUINullRect(w: 20, h: 20) });
          block.Add(new CUITextBlock("qiwejfpoqwenfqwpeifnqw"));

          return block;
        }

        CUIComponent CreateBlock2()
        {
          CUIVerticalList block = new CUIVerticalList()
          {
            FitContent = new CUIBool2(true, true),
            Absolute = new CUINullRect(x: 20, y: 150),
          };

          CUIComponent a = block["nested"] = new CUIHorizontalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent b = a["nested"] = new CUIVerticalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent c = b["nested"] = new CUIHorizontalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent d = c["nested"] = new CUIVerticalList() { FitContent = new CUIBool2(true, true) };

          d.Children.Add(new CUITextBlock("123"));
          d.Children.Add(new CUIComponent() { Absolute = new CUINullRect(w: 20, h: 20) });
          d.Children.Add(new CUITextBlock("qiwejfpoqwenfqwpeifnqw"));

          return block;
        }

        CUIComponent CreateBlock3()
        {
          CUIVerticalList block = new CUIVerticalList()
          {
            FitContent = new CUIBool2(true, true),
            Absolute = new CUINullRect(x: 20, y: 250),
          };

          CUIComponent a = block["nested"] = new CUIHorizontalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent b = a["nested"] = new CUIHorizontalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent c = b["nested"] = new CUIHorizontalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent d = c["nested"] = new CUIVerticalList() { FitContent = new CUIBool2(true, true) };

          d.Children.Add(new CUITextBlock("123"));
          d.Children.Add(new CUIComponent() { Absolute = new CUINullRect(w: 20, h: 20) });
          d.Children.Add(new CUITextBlock("qiwejfpoqwenfqwpeifnqw"));

          return block;
        }

        CUIComponent CreateBlock4()
        {
          CUIHorizontalList block = new CUIHorizontalList()
          {
            FitContent = new CUIBool2(true, true),
            Absolute = new CUINullRect(x: 20, y: 350),
          };

          CUIComponent a = block["nested"] = new CUIVerticalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent b = a["nested"] = new CUIHorizontalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent c = b["nested"] = new CUIVerticalList() { FitContent = new CUIBool2(true, true) };
          CUIComponent d = c["nested"] = new CUIHorizontalList() { FitContent = new CUIBool2(true, true) };

          d.Children.Add(new CUITextBlock("123"));
          d.Children.Add(new CUIComponent() { Absolute = new CUINullRect(w: 20, h: 40) });
          d.Children.Add(new CUITextBlock("qiwejfpoqwe"));

          return block;
        }



        using (new CUIContextStyle<CUIComponent>(c => c.Background.Color = Color.Cyan))
        using (new CUIContextStyle<CUITextBlock>(c => c.Background.Color = Color.Green))
        using (new CUIContextStyle<CUIVerticalList>(c => c.Background.Color = Color.Orange))
        using (new CUIContextStyle<CUIHorizontalList>(c => c.Background.Color = Color.Pink))
        {
          frame["block 1"] = CreateBlock1();
          frame["block 2"] = CreateBlock2();
          frame["block 3"] = CreateBlock3();
          frame["block 4"] = CreateBlock4();
        }

        return frame;
      }
    }
  }
}