using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      public static CUIComponent MoreResizeHandles()
      {
        CUIComponent component = new CUIDefault.Panel()
        {
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(w: 400, h: 400),
          ResizableBoth = true,
          Draggable = true,
          AbsoluteMin = new CUINullRect(ResizeHandle.DefaultSize),
        };

        component.Children.Add(new ResizeHandle(0.0f, 0.0f));
        component.Children.Add(new ResizeHandle(1.0f, 0.0f));


        component.Children.Add(new ResizeHandle(0.3f, 0.3f));
        component.Children.Add(new ResizeHandle(0.7f, 0.3f));
        component.Children.Add(new ResizeHandle(0.7f, 0.7f));
        component.Children.Add(new ResizeHandle(0.3f, 0.7f));


        component.Children.Add(new ResizeHandle(-0.5f, -0.5f));
        component.Children.Add(new ResizeHandle(1.5f, -0.5f));
        component.Children.Add(new ResizeHandle(1.5f, 1.5f));
        component.Children.Add(new ResizeHandle(-0.5f, 1.5f));

        using (new CUIContextStyle<ResizeHandle>(c =>
        {
          c.Background.Sprite = CUISprite.Vignette;
          c.Absolute = new CUINullRect(w: 20, h: 60);
        }))
        {
          component.Children.Add(new ResizeHandle(0.0f, 0.6f)
          {
            Background = { Sprite = CUISprite.Vignette }
          });
          component.Children.Add(new ResizeHandle(1.0f, 0.6f));
        }

        return component;
      }
    }
  }
}