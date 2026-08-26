using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public abstract class VisualUnit
  {
    public override string ToString() => this.GetType().Name;


    public class PrimitiveVisualElement : VisualUnit
    {
      public bool Contains(Vector2 pos) => Element.Contains(pos);

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
  }
}