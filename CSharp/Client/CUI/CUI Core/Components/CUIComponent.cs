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
  public partial class CUIComponent : CUIVisualComponent
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


    public event Action OnClick
    {
      add => Background.OnClick += value;
      remove => Background.OnClick -= value;
    }

    protected SimpleTexture Background { get; } = new();

    public Color BackgroundColor
    {
      get => Background.Color;
      set => Background.Color = value;
    }

    public Rectangle Rect
    {
      get => Background.Rect;
      protected set => Background.Rect = value;
    }


    protected Rectangle? absolute; public Rectangle? Absolute
    {
      set
      {
        absolute = value;
        if (absolute.HasValue) Rect = absolute.Value;
      }
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      yield return new VisualUnit.PrimitiveVisualElement(Background);
      yield return new VisualUnit.LeftContextBound();
      foreach (CUIVisualComponent child in children)
      {
        yield return new VisualUnit.NestedVisualComponent(child);
      }
      yield return new VisualUnit.RightContextBound();
    }


  }
}