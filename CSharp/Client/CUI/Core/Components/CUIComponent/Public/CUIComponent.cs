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
  public partial class CUIComponent : IComponent
  {




    public bool MouseOver => Events.MouseOver;
    public bool MousePressed => Events.MousePressed;


    public Layout Layout
    {
      get => LayoutSlot.Layout;
      set => LayoutSlot.Layout = value;
    }
    [CUISerializable]
    public Color BackgroundColor
    {
      get => Background.Color;
      set => Background.Color = value;
    }


  }
}