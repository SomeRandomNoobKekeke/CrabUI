using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class CUIPropsDebug : CUIFrame
  {
    static CUIPropsDebug() { CUI.OnDispose += () => { Instance = null; }; }
    public static CUIPropsDebug Instance;
    public static bool Opened => Instance?.Parent != null;
    public static CUIPropsDebug Open()
    {
      if (!Opened)
      {
        Instance = new CUIPropsDebug();
        CUI.TopMain.Append(Instance);
      }

      return Instance;
    }

    public CUIComponentSelector Selector;
    public CUIPropsInterface Props;

    public CUIPropsDebug() : base()
    {
      Absolute = new CUINullRect(w: 600, h: 400);
      Anchor = new Vector2(0.3f, 0.5f);

      this["layout"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };
      this["layout"]["handle"] = CUIPrefab.FormHandle("Prop Debug");
      this["layout"]["main"] = new CUIHorizontalList()
      {
        FillEmptySpace = new CUIBool2(false, true),
        ConsumeDragAndDrop = true,
      };

      this["layout"]["main"]["select"] = Selector = new CUIComponentSelector()
      {
        FitContent = new CUIBool2(true, false),
        DeepPalette = PaletteOrder.Secondary,
        BackgroundColor = new Color(0, 0, 0),
      };
      this["layout"]["main"]["props"] = Props = new CUIPropsInterface()
      {
        FillEmptySpace = new CUIBool2(true, false),
        DeepPalette = PaletteOrder.Secondary,
      };

      Selector.OnSelect += (c) => Props.Target = c;

      Props.Target = this;
      IgnoreDebug = true;


      Selector.Refresh();
    }
  }
}