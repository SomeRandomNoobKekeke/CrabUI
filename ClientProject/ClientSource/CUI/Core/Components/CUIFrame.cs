using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIFrame : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIFrame>((frame) =>
    {
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
    }


    public void Close() => RemoveSelf();
  }
}