using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Simple dialog box with a message and ok button  
  /// use public static void Open(string msg) to open it
  /// </summary>
  public class CUIMessageBox : CUIFrame
  {
    public static void Open(string msg)
    {
      CUI.TopMain.Append(new CUIMessageBox(msg));
    }


    public CUIMessageBox(string msg) : base()
    {
      Palette = PaletteOrder.Quaternary;
      Resizible = false;

      Relative = new CUINullRect(0, 0, 0.25f, 0.25f);
      Anchor = CUIAnchor.Center;

      OutlineThickness = 2;

      this["layout"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      this["layout"]["main"] = new CUIVerticalList()
      {
        FillEmptySpace = new CUIBool2(false, true),
        Scrollable = true,
        ScrollSpeed = 0.5f,
        Style = CUIStylePrefab.Main,
      };

      this["layout"]["main"]["msg"] = new CUITextBlock(msg)
      {
        TextScale = 1.2f,
        Wrap = true,
        Font = GUIStyle.Font,
        TextAlign = CUIAnchor.TopCenter,
        Style = new CUIStyle()
        {
          ["Padding"] = "[10,10]",
        },
      };
      this["layout"]["ok"] = new CUIButton("Ok")
      {
        TextScale = 1.0f,
        Style = new CUIStyle()
        {
          ["Padding"] = "[10,10]",
        },
        AddOnMouseDown = (e) => this.RemoveSelf(),
      };
    }


  }
}