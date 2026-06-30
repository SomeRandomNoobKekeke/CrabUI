using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  public partial class CUIFrame : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIFrame>((c) =>
    {
      c.Background.Color = c.Palette.Colors["frame"];
      c.Borders.Color = c.Palette.Colors["border"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      InnerAbsoluteMin = new CUINullRect(w: ResizeHandle.DefaultSize.X, h: ResizeHandle.DefaultSize.Y);
      Draggable = true;
      CullChildren = true;
      Resizable = true;
      Anchor = CUIAnchor.Center;
      Background.Sprite = CUIDefaultSprite.Vignette;
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

      Host.Append(this);
      OnOpen?.Invoke(this);
      SaveState("lastopened");
    }


    public void Close() => RemoveSelf();
    public event Action<CUIFrame> OnOpen;

    public CUIFrame() : base()
    {
      Commands.ListenFor("close", (_) => Close());
      MouseDoubleClick += (c, e) => RestoreState("lastopened");
    }
  }
}