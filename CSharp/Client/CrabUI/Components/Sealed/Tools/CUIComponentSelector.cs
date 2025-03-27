using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CrabUI
{
  public class CUIComponentSelector : CUIVerticalList, IRefreshable
  {
    public class Button : CUIToggleButton
    {
      public CUIComponent Target;
      public Button(CUIComponent target) : base()
      {
        Target = target;
        Text = target.ToString();
        TextAlign = CUIAnchor.CenterLeft;
        ToggleOnClick = false;
        IgnoreDebug = true;
        OnMouseDown += (e) => DispatchUp(new CUICommand("target selected", Target));
        OnMouseEnter += (e) => { if (Target != null) Target.DebugHighlight = true; };
        OnMouseLeave += (e) => { if (Target != null) Target.DebugHighlight = false; };
      }
    }

    private CUIComponent selected;
    public CUIComponent Selected
    {
      get => selected;
      set
      {
        selected = value;
        OnSelect?.Invoke(value);
      }
    }
    public event Action<CUIComponent> OnSelect;

    public void Refresh()
    {
      //Stopwatch sw = Stopwatch.StartNew();
      RemoveAllChildren();

      List<CUIComponent> all = CUIComponent.AllComponents.Where(c => !c.IgnoreDebug).ToList();
      foreach (CUIComponent c in all)
      {
        this.Append(new Button(c)
        {
          State = c == Selected,
          Palette = this.Palette, //GUH
        });
      }

      //CUI.Log($"CUIComponentSelector Refresh took {sw.ElapsedMilliseconds} ms");
    }

    public CUIComponentSelector() : base()
    {
      Scrollable = true;
      IgnoreDebug = true;

      AddCommand("target selected", o =>
      {
        if (o is CUIComponent c)
        {
          Selected = c;
          Refresh();
        }
      });

      OnMouseDown += (e) => Refresh();

      // I think this should be called in the component it embeded in
      //Refresh();
    }
  }
}