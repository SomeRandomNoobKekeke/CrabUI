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
  public class CUIPropsInterface : CUIComponent, IRefreshable
  {
    private CUIComponent target;
    public CUIComponent Target
    {
      get => target;
      set
      {
        target = value;
        Refresh();
      }
    }
    public CUIVerticalList Props { get; set; }

    public void Refresh()
    {
      Props.RemoveAllChildren();

      if (Target == null) return;

      CUITypeMetaData meta = CUITypeMetaData.Get(Target.GetType());

      foreach (string key in meta.Serializable.Keys)
      {
        Props.Append(new CUITextBlock($"{key} - {meta.Serializable[key].GetValue(Target)}")
        {
          IgnoreDebug = true,
        });
      }
    }

    public CUIPropsInterface() : base()
    {
      SerializeChildren = false;
      this["Props"] = Props = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        Scrollable = true,
      };

      IgnoreDebug = true;
    }
  }
}