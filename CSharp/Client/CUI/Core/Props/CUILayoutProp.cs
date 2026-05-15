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
  public interface ICUILayoutProp : IProp
  {
    public interface IContainer : IPropContainer
    {

      public void Mark(LayoutMarker.Pattern Pattern);
    }
  }

  public class CUILayoutProp<T> : CUIProp<T>, ICUILayoutProp
  {
    public DebugNode<Type, object, object, string> Debug_ValueSet { get; } = new(
      "Layout", CUI.DebugHub,
      (propType, value, host, propName) => $"{host}.{propName} = {value}"
    );

    public LayoutMarker.Pattern Pattern { get; set; } = LayoutMarker.Pattern.None;

    [In] public ICUILayoutProp.IContainer Container { get; set; }

    public override T Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        Container.Mark(Pattern);
        Debug_ValueSet.Send(typeof(T), value, HostComponent, HostPropName);
      }
    }
  }
}