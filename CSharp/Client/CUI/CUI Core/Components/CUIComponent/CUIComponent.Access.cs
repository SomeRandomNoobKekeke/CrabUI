using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIComponent : CUIVisualComponent, IMouseEventConsumer
  {
    public class Access
    {
      public CUIComponent Component;

      public Rectangle Rect
      {
        get => Component.Rect;
        set => Component.Rect = value;
      }


      public Access(CUIComponent component) => Component = component;
    }


  }
}