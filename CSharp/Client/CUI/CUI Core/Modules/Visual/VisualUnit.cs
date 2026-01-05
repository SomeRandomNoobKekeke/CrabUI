using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public interface VisualUnit
  {
    public class PrimitiveVisualElement : VisualUnit
    {
      public IVisualElement Element;
      public PrimitiveVisualElement(IVisualElement element) => Element = element;
    }

    public class NestedVisualComponent : VisualUnit
    {
      public IVisualComponent Component;
      public NestedVisualComponent(IVisualComponent component) => Component = component;
    }

    public class LeftContextBound : VisualUnit
    {

    }

    public class RightContextBound : VisualUnit
    {

    }
  }
}