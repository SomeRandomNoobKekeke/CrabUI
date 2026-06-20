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
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIFrame>((frame) =>
    {
      frame.AbsoluteMin = new CUINullRect(w: 15, h: 10);
      frame.Draggable = true;
    });

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
    }


    public void Close() => RemoveSelf();
    public event Action<CUIFrame> OnOpen;

    public CUIFrame() : base()
    {
      Commands.ListenFor("close", (_) => Close());
    }
  }
}