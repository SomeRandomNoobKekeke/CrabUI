using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  public partial class CUIFrame : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIFrame>((c) =>
    {
      c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.2f);
      c.Borders.Color = c.Palette["main"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      AbsoluteMin = new CUINullRect(w: ResizeHandle.DefaultSize.X, h: ResizeHandle.DefaultSize.Y);
      Draggable = true;
      CullChildren = true;
      Resizable = true;
      Focusable = true;
      ConsumeFocus = true;
      ConsumeMouseEvents = true;
      Background.Sprite = CUISprite.Vignette;
    }

    public CUIComponent TargetMainComponent { get; set; }



    public bool IsOpen
    {
      get => Parent != null;
      set
      {
        if (value) Open(); else Close();
      }
    }

    public void Open(CUIComponent Host = null)
    {
      Host ??= TargetMainComponent ?? CUI.Main;
      if (Host == null || Parent == Host) return;

      Host.Children.Add(this);
      OnOpen?.Invoke(this);
      SaveState("lastopened");
    }


    public void Close()
    {
      RemoveSelf();
      OnClose?.Invoke(this);
    }
    public event Action<CUIFrame> OnOpen;
    public event Action<CUIFrame> OnClose;

    public CUIFrame() : base()
    {
      Commands.ListenFor("close", (_) => Close());
      MouseDoubleClick += (e) => RestoreState("lastopened");
      OnFocus += MoveToTop;
    }
  }
}