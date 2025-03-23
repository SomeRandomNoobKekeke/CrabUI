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

    public CUIPropsInterface Props;

    public CUIPropsDebug() : base()
    {
      Absolute = new CUINullRect(0, 0, 200, 400);
      Anchor = new Vector2(0.2f, 0.5f);

      CUIPrefab.ListFrameWithHeader(this);
      this["content"]["props"] = Props = new CUIPropsInterface()
      {
        Relative = new CUINullRect(0, 0, 1, 1)
      };
      Props.Target = this;

      AddCommand("close frame", (o) => RemoveSelf());
    }
  }
}