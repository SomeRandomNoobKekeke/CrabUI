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
    public bool IsOpen => Parent != null;

    public void Open(CUIComponent Host = null)
    {
      Host ??= CUI.Main;
      if (Host == null || Parent == Host) return;

      Host.Append(this);
    }


    public void Close()
    {
      RemoveSelf();
    }

    public CUIFrame() : base()
    {
      this.Draggable = true;
    }
  }
}