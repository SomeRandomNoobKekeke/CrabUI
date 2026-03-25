using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public abstract class VisualUnit
  {
    public class PrimitiveVisualElement : VisualUnit
    {
      public IVisualElement Element;
      public PrimitiveVisualElement(IVisualElement element) => Element = element;
      public override string ToString() => Element.ToString();
    }

    public class NestedVisualComponent : VisualUnit
    {
      public IVisualComponent Component;
      public NestedVisualComponent(IVisualComponent component) => Component = component;
      public override string ToString() => Component.ToString();
    }

    public class LeftContextBound : VisualUnit { }
    public class RightContextBound : VisualUnit { }
  }
}