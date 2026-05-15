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
  public partial class CUIComponent
  {
    protected FunnyProps_Part FunnyProps { get; } = new();
    public class FunnyProps_Part : Part
    {
      public DebugNode<Type, object, CUIComponent, string> Debug_PropSet { get; } = new(
        "Funny Prop Set", CUI.DebugHub,
        (propType, value, host, propName) => $"{host}.{propName} = {value}"
      );

      public void Init()
      {
        Debug_PropSet.Map(Self.DebugRelays["Prop Set"]);

        Rect.OnValueSet = (rect) =>
        {
          Self.UpdateRect(rect);
          Debug_PropSet.Send(typeof(CUIRect), rect, Self, "Rect");
        };
      }

      public CUIReactiveProp<CUIRect> Rect { get; set; } = new();
    }
  }
}