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
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;

namespace CursedUI
{
  public partial class CUIFrame : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle => new CUIDefaultStyle<CUIFrame>((c) =>
    {
      c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.2f);
      c.Border.Color = c.Palette["main"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      AbsoluteMin = new CUINullRect(w: ResizeHandle.DefaultSize.X, h: ResizeHandle.DefaultSize.Y);
      Draggable = true;
      CullChildren = true;
      Resizable = true;
      Focusable = true;
      ConsumeMouseEvents = true;
      Background.Sprite = CUISprite.VignetteDithered;
    }

    protected static ConditionalWeakTable<Type, CUIFrame> OpenedFrames { get; } = new();


    public CUIComponent TargetMainComponent { get; set; }

    protected virtual bool SingleInstance => false;

    public bool IsOpen
    {
      get => Parent != null;
      set
      {
        if (value) Open(); else Close();
      }
    }

    public void Toggle() { if (IsOpen) Close(); else Open(); }


    public void Open(Vector2 pos, CUIComponent Host = null)
    {
      Absolute = Absolute with { Position = pos };
      Open(Host);
    }
    public void Open(CUIComponent Host = null)
    {
      if (SingleInstance)
      {
        OpenedFrames.TryGetValue(this.GetType(), out CUIFrame frame);
        frame?.Close();

        OpenedFrames.Add(this.GetType(), this);
      }


      Host ??= TargetMainComponent ?? CUI.Main;

      if (Host == null || Parent == Host) return;

      Host.Children.Add(this);
      OnOpen?.Invoke();
      SaveState("lastopened");
    }

    public virtual void Close()
    {
      if (SingleInstance)
      {
        OpenedFrames.Remove(this.GetType());
      }

      OnClose?.Invoke();
      RemoveSelf();
    }
    public event Action OnOpen;
    public event Action OnClose;

    public CUIFrame() : base()
    {
      Commands.ListenFor("close", (_) => Close());
      MouseDoubleClick += (e) => RestoreState("lastopened");
      OnFocus += MoveToTop;
    }
  }
}