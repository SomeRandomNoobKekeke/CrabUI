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
  public partial class CUIComponent
  {
    protected FunnyProps_Part FunnyProps { get; } = new();
    public class FunnyProps_Part : Part
    {
      public void Init()
      {
        Rect.OnValueSet = (rect) =>
        {
          Self.UpdateRect(rect);
        };
      }

      public CUIReactiveProp<CUIRect> Rect { get; set; } = new();
    }
  }
}