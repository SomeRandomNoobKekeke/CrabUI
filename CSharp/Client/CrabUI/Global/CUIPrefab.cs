using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;

namespace CrabUI
{
  public static class CUIStylePrefab
  {
    public static CUIStyle FrameCaption => new CUIStyle()
    {
      {"BackgroundColor", "CUIPalette.Frame.Border"},
      {"Border", "CUIPalette.Frame.Border"},
      {"TextColor", "CUIPalette.Frame.Text"},
    };

    public static CUIStyle Header => new CUIStyle()
    {
      {"BackgroundColor", "CUIPalette.Header.Background"},
      {"Border", "CUIPalette.Header.Border"},
      {"TextColor", "CUIPalette.Header.Text"},
    };

    public static CUIStyle Nav => new CUIStyle()
    {
      {"BackgroundColor", "CUIPalette.Nav.Background"},
      {"Border", "CUIPalette.Nav.Border"},
      {"TextColor", "CUIPalette.Nav.Text"},
    };

    public static CUIStyle Main => new CUIStyle()
    {
      {"BackgroundColor", "CUIPalette.Main.Background"},
      {"Border", "CUIPalette.Main.Border"},
      {"TextColor", "CUIPalette.Main.Text"},
    };
  }

  public static class CUIPrefab
  {
    public static CUIFrame ListFrame()
    {
      CUIFrame frame = new CUIFrame();
      frame["list"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1), };
      return frame;
    }

    public static CUIFrame ListFrameWithHeader()
    {
      CUIFrame frame = new CUIFrame() { };
      frame["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1), };
      frame["layout"]["handle"] = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(false, true),
        Direction = CUIDirection.Reverse,
        Style = CUIStylePrefab.FrameCaption,
      };

      frame["layout"]["handle"]["close"] = new CUICloseButton()
      {
        Absolute = new CUINullRect(0, 0, 15, 15),
        Command = "close frame",
      };
      frame["layout"]["handle"]["caption"] = new CUITextBlock("Caption") { FillEmptySpace = new CUIBool2(true, false) };
      frame["layout"]["header"] = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(false, true),
        Style = CUIStylePrefab.Header,
      };
      frame["layout"]["content"] = new CUIVerticalList()
      {
        FillEmptySpace = new CUIBool2(false, true),
        Style = CUIStylePrefab.Main,
        Scrollable = true,
        ConsumeDragAndDrop = true,
        ConsumeMouseClicks = true,
      };

      frame["header"] = frame["layout"]["header"];
      frame["content"] = frame["layout"]["content"];
      frame["caption"] = frame["layout"]["handle"]["caption"];
      return frame;
    }



    public static CUIHorizontalList TickboxWithLabel(float tickboxSize, string text)
    {
      CUIHorizontalList list = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(true, true),
        Border = new CUIBorder(),
      };

      list["tickbox"] = new CUITickBox()
      {
        Absolute = new CUINullRect(w: tickboxSize, h: tickboxSize),
        Command = text,
      };

      list["text"] = new CUITextBlock()
      {
        Text = text,
        TextAlign = CUIAnchor.CenterLeft,
      };

      return list;
    }

    //TODO this is now too specific and shouldn't be here
    public static CUIHorizontalList InputWithValidation(PropertyInfo pi, string command)
    {
      string ToUserFriendly(Type T)
      {
        if (T == typeof(bool)) return "Boolean";
        if (T == typeof(int)) return "Integer";
        if (T == typeof(float)) return "Float";
        return T.Name;
      }

      CUIHorizontalList list = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(true, true),
        Border = new CUIBorder(),
      };

      list["input"] = new CUITextInput()
      {
        AbsoluteMin = new CUINullRect(w: 100),
        Relative = new CUINullRect(w: 0.3f),
        Command = command,
        Consumes = command,
        VatidationType = pi.PropertyType,
      };

      list["label"] = new CUITextBlock()
      {
        FillEmptySpace = new CUIBool2(true, false),
        Text = $"{ToUserFriendly(pi.PropertyType)} {pi.Name}",
        TextAlign = CUIAnchor.CenterLeft,
        BackgroundSprite = new CUISprite("gradient.png"),

        Style = new CUIStyle(){
          {"BackgroundColor", "CUIPalette.Text3.Background"},
          {"Border", "CUIPalette.Text3.Border"},
          {"TextColor", "CUIPalette.Text3.Text"},
        },
      };

      return list;
    }


  }
}