using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class VI
  {
    public interface VisualFlattenerInstruction
    {

    }

    public interface VisualUnit
    {

    }

    public class PrimitiveVisualElement : VisualFlattenerInstruction, VisualUnit
    {
      public IVisualElement Element;
      public PrimitiveVisualElement(IVisualElement element) => Element = element;
    }

    public class NestedVisualComponent : VisualFlattenerInstruction
    {
      public IVisualComponent Component;
      public NestedVisualComponent(IVisualComponent component) => Component = component;
    }

    public class LeftContextBound : VisualFlattenerInstruction, VisualUnit
    {

    }

    public class RightContextBound : VisualFlattenerInstruction, VisualUnit
    {

    }
  }
}