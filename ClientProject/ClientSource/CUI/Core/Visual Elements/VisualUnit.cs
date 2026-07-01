using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public abstract class VisualUnit : IAware
  {
    public abstract object HostComponent { get; set; }
    public abstract string HostPropName { get; set; }
    public override string ToString() => this.GetType().Name;


    public class PrimitiveVisualElement : VisualUnit
    {
      public override object HostComponent
      {
        get => Element.HostComponent;
        set => Element.HostComponent = value;
      }
      public override string HostPropName
      {
        get => Element.HostPropName;
        set => Element.HostPropName = value;
      }

      public bool Contains(Vector2 pos) => Element.Contains(pos);

      public IVisualElement Element;
      public PrimitiveVisualElement(IVisualElement element) => Element = element;
      public override string ToString() => Element.ToString();
    }

    public class NestedVisualComponent : VisualUnit
    {
      public override object HostComponent
      {
        get => Component;
        set { }
      }
      public override string HostPropName { get; set; }

      public IVisualComponent Component;
      public NestedVisualComponent(IVisualComponent component) => Component = component;
      public override string ToString() => Component.ToString();
    }
  }
}